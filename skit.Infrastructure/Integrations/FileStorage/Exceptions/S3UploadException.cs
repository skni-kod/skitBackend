using skit.Shared.Abstractions.Exceptions;

namespace skit.Infrastructure.Integrations.FileStorage.Exceptions;

public sealed class S3UploadException : SkitException
{
    public S3UploadException(string errorCode) : base($"Unexpected error while uploading file to S3Storage: {errorCode}") { }
}