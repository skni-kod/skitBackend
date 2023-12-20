using skit.Shared.Abstractions.Exceptions;

namespace skit.Infrastructure.Integrations.FileStorage.Exceptions;

public sealed class S3GetUrlException : SkitException
{
    public S3GetUrlException(string errorCode) : base($"Unexpected error while getting URL file from S3Storage: {errorCode}") { }
}