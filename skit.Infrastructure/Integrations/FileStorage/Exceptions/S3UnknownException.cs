using skit.Shared.Abstractions.Exceptions;

namespace skit.Infrastructure.Integrations.FileStorage.Exceptions;

public sealed class S3UnknownException : SkitException
{
    public S3UnknownException() : base("Unknown exception while connecting to S3Service") { }
}