using MediatR;
using skit.Application.Companies.Queries.BrowseCompanies.DTO;
using skit.Application.Companies.Queries.DTO;
using skit.Core.Companies.Enums;
using skit.Shared.Abstractions.Queries;

namespace skit.Application.Companies.Queries.BrowseCompanies;

public sealed record BrowseCompaniesQuery(string? Search, CompanySize? Size) : PaginationRequest, IRequest<BrowseCompaniesDto>;