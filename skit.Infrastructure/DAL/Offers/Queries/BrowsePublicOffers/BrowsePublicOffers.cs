using MediatR;
using skit.Application.Offers.Queries.BrowsePublicOffers;
using skit.Core.Offers.Enums;
using skit.Infrastructure.DAL.EF.Context;
using skit.Shared.Extensions;

namespace skit.Infrastructure.DAL.Offers.Queries.BrowsePublicOffers;

internal sealed class BrowsePublicOffers : IRequestHandler<BrowsePublicOffersQuery, BrowsePublicOffersResponse>
{
    private readonly EFContext _context;

    public BrowsePublicOffers(EFContext context)
    {
        _context = context;
    }

    public async Task<BrowsePublicOffersResponse> Handle(BrowsePublicOffersQuery query, CancellationToken cancellationToken)
    {
        var offers = _context.Offers.AsNoTracking().Where(offer => offer.Status == OfferStatus.Public);

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

        return new BrowsePublicOffersResponse(result);
    }
}