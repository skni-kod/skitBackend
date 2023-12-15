using MediatR;
using skit.Core.Companies.Enums;
using skit.Shared.Abstractions.Models;

namespace skit.Application.Companies.Queries.Public.BrowseCompanies;

public sealed record BrowseCompaniesQuery(string? Search, CompanySize? Size) : PaginationRequest, IRequest<BrowseCompaniesResponse>;
