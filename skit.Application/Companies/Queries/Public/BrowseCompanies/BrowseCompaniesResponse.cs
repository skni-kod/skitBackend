using skit.Application.Companies.Queries.DTO;
using skit.Shared.Models;

namespace skit.Application.Companies.Queries.Public.BrowseCompanies;

public sealed record BrowseCompaniesResponse(PaginatedList<CompanyDto> Companies);