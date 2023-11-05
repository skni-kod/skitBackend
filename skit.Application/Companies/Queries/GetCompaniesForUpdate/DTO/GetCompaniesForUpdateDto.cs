using skit.Core.Companies.Enums;

namespace skit.Application.Companies.Queries.GetCompaniesForUpdate.DTO;

public sealed class GetCompaniesForUpdateDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public CompanySize Size { get; set; }
    public string? Links { get; set; }
}