using MediatR;
using Microsoft.EntityFrameworkCore;
using skit.Application.Companies.Queries.DTO;
using skit.Application.Companies.Queries.Public.BrowseCompanies;
using skit.Core.Files.Services;
using skit.Infrastructure.DAL.EF.Context;
using skit.Shared.Abstractions;
using skit.Shared.Extensions;

namespace skit.Infrastructure.DAL.Companies.Queries.BrowseCompanies;

internal sealed class BrowseCompaniesHandler : IRequestHandler<BrowseCompaniesQuery, BrowseCompaniesResponse>
{
    private readonly EFContext _context;
    private readonly IS3StorageService _s3StorageService;

    public BrowseCompaniesHandler(EFContext context, IS3StorageService s3StorageService)
    {
        _context = context;
        _s3StorageService = s3StorageService;
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
            .Include(x => x.Image)
            .Select(c => c.AsDto(c.ImageId == null ? null : _s3StorageService.GetFileUrl(c.Image.S3Key, c.Image.Name)))
            .ToPaginatedListAsync(query, cancellationToken);

        return new BrowseCompaniesResponse(result);
    }
}