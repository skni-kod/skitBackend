using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Companies.Exceptions;

public sealed class CompanyWithNameExistsException : SkitException
{
    public CompanyWithNameExistsException() : base("Company with that name exists") { }
}