using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Identity.Exceptions;

public sealed class UserNotFoundException : SkitException
{
    public UserNotFoundException() : base("User not found") { }
}