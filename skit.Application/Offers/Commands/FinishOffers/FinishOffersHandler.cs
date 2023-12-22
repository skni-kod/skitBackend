using MediatR;
using skit.Core.Offers.Enums;
using skit.Core.Offers.Repositories;

namespace skit.Application.Offers.Commands.FinishOffers;

public class FinishOffersHandler : IRequestHandler<FinishOffersCommand>
{
    private readonly IOfferRepository _offerRepository;

    public FinishOffersHandler(IOfferRepository offerRepository)
    {
        _offerRepository = offerRepository;
    }

    public async Task Handle(FinishOffersCommand request, CancellationToken cancellationToken)
    {
        var offers = await _offerRepository.GetAllAsync(cancellationToken);

        foreach (var offer in offers)
        {
            if (offer.DateTo < DateTimeOffset.Now)
            {
                offer.ChangeStatus(OfferStatus.Finished);
                await _offerRepository.UpdateAsync(offer, cancellationToken);
            }
        }
    }
}