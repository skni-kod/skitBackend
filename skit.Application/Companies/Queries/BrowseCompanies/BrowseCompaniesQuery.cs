using MediatR;
using skit.Application.Companies.Queries.DTO;
using skit.Core.Companies.Enums;

namespace skit.Application.Companies.Queries.BrowseCompanies;

public class BrowseCompaniesQuery : IRequest<List<CompanyDto>>
{
    public string? Name { get; set; }
    public CompanySize? Size { get; set; }
}