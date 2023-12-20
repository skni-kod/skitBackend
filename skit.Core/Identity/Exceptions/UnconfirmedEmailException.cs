using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Identity.Exceptions;

public sealed class UnconfirmedEmailException : SkitException
{
    public UnconfirmedEmailException() : base("User email in unconfirmed") { }
}