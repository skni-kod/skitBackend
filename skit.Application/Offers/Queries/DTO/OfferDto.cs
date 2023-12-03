using skit.Core.Addresses.Entities;
using skit.Core.Offers.Enums;
using skit.Core.Salaries.Enums;

namespace skit.Application.Offers.Queries.DTO;

public sealed class OfferDto
{
    public string Title { get; set; }
    public string CompanyName { get; set; }
    public List<OfferWorkLocation> WorkLocation { get; set; }
    public List<string> Cities { get; set; }
    public List<SalaryDto> Salaries { get; set; }
    public List<TechnologyDto> Technologies { get; set; }
}

public sealed class SalaryDto
{
    public decimal SalaryFrom { get; set; }
    public decimal? SalaryTo { get; set; }
    public SalaryEmploymentType EmploymentType { get; set; }
}

public sealed class TechnologyDto
{
    public string Name { get; set; }
    public string? ThumUrl { get; set; }
}