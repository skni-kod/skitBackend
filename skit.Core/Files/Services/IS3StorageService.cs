using Microsoft.AspNetCore.Http;

namespace skit.Core.Files.Services;

public interface IS3StorageService
{
    Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken);
    string GetFileUrl(string fileKey, string fileName);
}