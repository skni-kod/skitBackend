using skit.Application.Companies.Queries.DTO;

namespace skit.Application.Companies.Queries.BrowseCompanies;

public sealed record BrowseCompaniesResponse(List<CompanyDto> Companies);