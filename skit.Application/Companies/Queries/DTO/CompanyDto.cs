using skit.Core.Companies.Enums;

namespace skit.Application.Companies.Queries.DTO;

public sealed class CompanyDto
{
    public string Name { get; set; }
    public CompanySize Size { get; set; }
}