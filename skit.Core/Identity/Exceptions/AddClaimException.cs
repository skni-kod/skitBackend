using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Identity.Exceptions;

public sealed class AddClaimException : SkitException
{
    public AddClaimException() : base("Error while adding claim to user") {}
}