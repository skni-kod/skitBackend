using skit.Application.Companies.Queries.DTO;

namespace skit.Application.Companies.Queries.BrowseCompanies.DTO;

public sealed record BrowseCompaniesDto(List<CompanyDto> Companies);