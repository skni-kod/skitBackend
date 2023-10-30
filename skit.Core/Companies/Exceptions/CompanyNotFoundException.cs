using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Companies.Exceptions;

public class CompanyNotFoundException : SkitException
{
    public CompanyNotFoundException(string message) : base(message)
    {
    }
}