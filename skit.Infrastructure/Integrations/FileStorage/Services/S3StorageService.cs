using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using skit.Core.Files.Services;
using skit.Infrastructure.Integrations.FileStorage.Configuration;
using skit.Infrastructure.Integrations.FileStorage.Exceptions;

namespace skit.Infrastructure.Integrations.FileStorage.Services;

public sealed class S3StorageService : IS3StorageService
{
    private readonly S3Config _s3Config;
    private readonly AmazonS3Client  _s3Client;

    public S3StorageService(S3Config s3Config)
    {
        _s3Config = s3Config;
        var connectionConfig = new AmazonS3Config
        {
            ServiceURL = _s3Config.S3Url,
            ForcePathStyle = true,
            UseHttp = false, // Use HTTPS
        };
        _s3Client = new AmazonS3Client(_s3Config.AccessKey, _s3Config.SecretKey, connectionConfig);
    }
    
    public async Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken)
    {
        var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";

        await using var stream = file.OpenReadStream();
        var request = new PutObjectRequest
        {
            BucketName = _s3Config.BucketName,
            Key = uniqueFileName,
            InputStream = stream,
            ContentType = file.ContentType
        };
        try
        {
            await _s3Client.PutObjectAsync(request, cancellationToken);
        }
        catch (AmazonS3Exception e)
        {
            throw new S3UploadException(e.ErrorCode);
        }
        catch (Exception e)
        {
            throw new S3UnknownException();
        }

        return uniqueFileName;
    }

    public string GetFileUrl(string fileKey, string fileName)
    {
        try
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _s3Config.BucketName,
                Key = fileKey,
                Expires = DateTime.UtcNow.Add(_s3Config.UrlExpires),
                ResponseHeaderOverrides = new ResponseHeaderOverrides
                {
                    ContentDisposition = $"attachment; filename=\"{fileName}\""
                }
            };

            var url = _s3Client.GetPreSignedURL(request);
            return url;
        }
        catch (AmazonS3Exception e)
        {
            throw new S3GetUrlException(e.ErrorCode);
        }
        catch (Exception e)
        {
            throw new S3UnknownException();
        }
    }
}