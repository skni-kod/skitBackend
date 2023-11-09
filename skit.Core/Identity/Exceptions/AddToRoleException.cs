using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Identity.Exceptions;

public sealed class AddToRoleException : SkitException
{
    public AddToRoleException() : base("Error while adding role to user") { }
}