using MediatR;
using skit.Application.Companies.Queries.DTO;
using skit.Core.Companies.Enums;
using skit.Shared.Abstractions.Models;

namespace skit.Application.Companies.Queries.BrowseCompanies;

public sealed record BrowseCompaniesQuery(string? Search, CompanySize? Size) : PaginationRequest, IRequest<BrowseCompaniesResponse>;
