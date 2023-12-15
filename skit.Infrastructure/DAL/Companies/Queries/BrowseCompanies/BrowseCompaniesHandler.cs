using MediatR;
using Microsoft.EntityFrameworkCore;
using skit.Application.Companies.Queries.BrowseCompanies;
using skit.Application.Companies.Queries.DTO;
using skit.Infrastructure.DAL.EF.Context;
using skit.Shared.Abstractions;
using skit.Shared.Extensions;

namespace skit.Infrastructure.DAL.Companies.Queries.BrowseCompanies;

internal sealed class BrowseCompaniesHandler : IRequestHandler<BrowseCompaniesQuery, BrowseCompaniesResponse>
{
    private readonly EFContext _context;

    public BrowseCompaniesHandler(EFContext context)
    {
        _context = context;
    }
    
    public async Task<BrowseCompaniesResponse> Handle(BrowseCompaniesQuery query, CancellationToken cancellationToken)
    {
        var companies = _context.Companies.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var searchTxt = $"%{query.Search}%";
            companies = companies
                .Where(company => EFCore.Functions.ILike(company.Name, searchTxt) || 
                    company.Description != null && EFCore.Functions.ILike(company.Description, searchTxt));
        }

        if (query.Size.HasValue)
        {
            companies = companies.Where(company => company.Size == query.Size);
        }

        var result = await companies
            .Select(company => company.AsDto())
            .ToPaginatedListAsync(query, cancellationToken);

        return new BrowseCompaniesResponse(result);
    }
}