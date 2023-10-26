using MediatR;
using Microsoft.EntityFrameworkCore;
using skit.Application.Companies.Queries.BrowseCompanies;
using skit.Application.Companies.Queries.DTO;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Companies.Queries.BrowseCompanies;

internal sealed class BrowseCompaniesHandler : IRequestHandler<BrowseCompaniesQuery, List<CompanyDto>>
{
    private readonly EFContext _context;

    public BrowseCompaniesHandler(EFContext context)
    {
        _context = context;
    }

    
    public Task<List<CompanyDto>> Handle(BrowseCompaniesQuery query, CancellationToken cancellationToken)
    {
        var companies = _context.Companies.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Name))
        {
            companies = companies.Where(company => company.Name.ToLower().Contains(query.Name.ToLower()));
        }

        if (query.Size.HasValue)
        {
            companies = companies.Where(company => company.Size == query.Size);
        }

        return companies
            .Select(company => company.AsDto())
            .ToListAsync(cancellationToken);
    }
}