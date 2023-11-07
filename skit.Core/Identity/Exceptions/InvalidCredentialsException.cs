using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Identity.Exceptions;

public sealed class InvalidCredentialsException : SkitException
{
    public InvalidCredentialsException() : base("Invalid credentials.") { }
}