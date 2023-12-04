using MediatR;
using skit.Application.Offers.Queries.GetOffer;
using skit.Core.Common.Services;
using skit.Core.Offers.Exceptions;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Offers.Queries.GetOffer;

internal sealed class GetOfferHandler : IRequestHandler<GetOfferQuery, GetOfferResponse>
{
    private readonly EFContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetOfferHandler(EFContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<GetOfferResponse> Handle(GetOfferQuery request, CancellationToken cancellationToken)
    {
        var offer = await _context.Offers.AsNoTracking()
            .Include(offer => offer.Addresses)
            .Include(offer => offer.Salaries)
            .Include(offer => offer.Company)
            .Include(offer => offer.Technologies)
            .Where(offer => offer.CompanyId == _currentUserService.CompanyId)
            .Where(offer => offer.Id == request.OfferId)
            .Select(offer => new GetOfferResponse(offer.AsDetailsDto()))
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new OfferNotFoundException();

        return offer;
    }
}