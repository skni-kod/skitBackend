using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Identity.Exceptions;

public sealed class InvalidRefreshTokenException : SkitException
{
    public InvalidRefreshTokenException() : base("Invalid refresh token") { }
}