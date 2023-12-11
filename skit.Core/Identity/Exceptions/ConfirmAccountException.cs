using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Identity.Exceptions;

public sealed class ConfirmAccountException : SkitException
{
    public ConfirmAccountException() : base("An error occurred while confirming the account.") { }
}