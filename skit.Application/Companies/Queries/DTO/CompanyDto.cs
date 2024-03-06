using skit.Core.Common.DTO;
using skit.Core.Companies.Enums;

namespace skit.Application.Companies.Queries.DTO;

public sealed class CompanyDto
{
    public string Name { get; set; }
    public BaseEnum? Size { get; set; }
    public string? ImageUrl { get; set; }
}