using MediatR;
using Microsoft.EntityFrameworkCore;
using skit.Application.Offers.Queries.BrowseOffers;
using skit.Core.Common.Services;
using skit.Core.Companies.Entities;
using skit.Infrastructure.DAL.EF.Context;
using skit.Shared.Abstractions;
using skit.Shared.Extensions;

namespace skit.Infrastructure.DAL.Offers.Queries.BrowseOffers;

internal sealed class BrowseOffersHandler : IRequestHandler<BrowseOffersQuery, BrowseOffersResponse>
{
    private readonly EFContext _context;
    private readonly ICurrentUserService _currentUserService;

    public BrowseOffersHandler(EFContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<BrowseOffersResponse> Handle(BrowseOffersQuery query, CancellationToken cancellationToken)
    {
        var offers = _context.Offers.AsNoTracking().Where(offer => offer.CompanyId == _currentUserService.CompanyId);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var searchTxt = $"%{query.Search}%";

            offers = offers.Where(offer => Microsoft.EntityFrameworkCore.EF.Functions.ILike(offer.Title, searchTxt) || 
                            offer.Description != null && Microsoft.EntityFrameworkCore.EF.Functions.ILike(offer.Description, searchTxt));
        }

        var result = await offers
            .Include(offer => offer.Addresses)
            .Include(offer => offer.Salaries)
            .Include(offer => offer.Company)
            .Include(offer => offer.Technologies)
            .Select(offer => offer.AsDto())
            .ToPaginatedListAsync(query, cancellationToken);

        return new BrowseOffersResponse(result);
    }
}