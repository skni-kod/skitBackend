using skit.Application.Companies.Queries.DTO;
using skit.Shared.Abstractions.Queries;

namespace skit.Application.Companies.Queries.BrowseCompanies.DTO;

public sealed record BrowseCompaniesDto(Paged<CompanyDto?> Companies);