using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Identity.Exceptions;

public sealed class UserWithEmailExistsException : SkitException
{
    public UserWithEmailExistsException() : base("User with that email already exists") {}
}