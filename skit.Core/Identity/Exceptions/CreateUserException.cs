using Microsoft.AspNetCore.Identity;
using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Identity.Exceptions;

public sealed class CreateUserException : SkitException
{
    public CreateUserException() : base("One or more erros occurred during creating user.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public CreateUserException(IEnumerable<IdentityError> errors) : this()
    {
        Errors = errors.GroupBy(e => e.Code, e => e.Description)
            .ToDictionary(a => a.Key, a => a.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}