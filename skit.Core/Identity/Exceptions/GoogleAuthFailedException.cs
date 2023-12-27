using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Identity.Exceptions;

public sealed class GoogleAuthFailedException : SkitException
{
    public GoogleAuthFailedException() : base("Google auth failed") { }
}