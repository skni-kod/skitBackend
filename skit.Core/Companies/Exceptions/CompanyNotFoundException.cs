using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Companies.Exceptions;

public class CompanyNotFoundException : SkitException
{
    public CompanyNotFoundException() : base("Company does not exist")
    {
    }
}