using MediatR;
using skit.Application.Companies.Queries.DTO;
using skit.Core.Companies.Enums;

namespace skit.Application.Companies.Queries.BrowseCompanies;

public sealed class BrowseCompaniesQuery : IRequest<BrowseCompaniesResponse>
{
    public string? Search { get; set; }
    public CompanySize? Size { get; set; }
}