using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Companies.Exceptions;

public sealed class CompanyExistsException : SkitException
{
    public CompanyExistsException() : base("Company exists") { }
}