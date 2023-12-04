using MediatR;
using skit.Application.Offers.Queries.GetPublicOffer;
using skit.Core.Common.Services;
using skit.Core.Offers.Enums;
using skit.Core.Offers.Exceptions;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Offers.Queries.GetPublicOffer;

internal sealed class GetPublicOfferHandler : IRequestHandler<GetPublicOfferQuery, GetPublicOfferResponse>
{
    private readonly EFContext _context;

    public GetPublicOfferHandler(EFContext context)
    {
        _context = context;
    }

    public async Task<GetPublicOfferResponse> Handle(GetPublicOfferQuery request, CancellationToken cancellationToken)
    {
        var offer = await _context.Offers.AsNoTracking()
                        .Include(offer => offer.Addresses)
                        .Include(offer => offer.Salaries)
                        .Include(offer => offer.Company)
                        .Include(offer => offer.Technologies)
                        .Where(offer => offer.Status == OfferStatus.Public)
                        .Where(offer => offer.Id == request.OfferId)
                        .Select(offer => new GetPublicOfferResponse(offer.AsDetailsDto()))
                        .SingleOrDefaultAsync(cancellationToken)
                    ?? throw new OfferNotFoundException();

        return offer;
    }
}