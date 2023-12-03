using MediatR;
using skit.Application.Offers.Queries.GetOfferForUpdate;
using skit.Core.Offers.Exceptions;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Offers.Queries.GetOfferForUpdate;

internal sealed class GetOfferForUpdateHandler : IRequestHandler<GetOfferForUpdateQuery, GetOfferForUpdateResponse>
{
    private readonly EFContext _context;

    public GetOfferForUpdateHandler(EFContext context)
    {
        _context = context;
    }

    public async Task<GetOfferForUpdateResponse> Handle(GetOfferForUpdateQuery request, CancellationToken cancellationToken)
    {
        var offer = await _context.Offers.AsNoTracking()
            .Include(offer => offer.Addresses)
            .Include(offer => offer.Salaries)
            .Include(offer => offer.Technologies)
            .Where(offer => offer.Id == request.OfferId)
            .Select(offer => new GetOfferForUpdateResponse(offer.AsGetOfferForUpdateDto()))
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new OfferNotFoundException();

        return offer;
    }
}