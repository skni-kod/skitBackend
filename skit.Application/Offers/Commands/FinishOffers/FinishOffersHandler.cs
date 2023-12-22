using MediatR;
using skit.Core.Common.Services;
using skit.Core.Offers.Enums;
using skit.Core.Offers.Repositories;

namespace skit.Application.Offers.Commands.FinishOffers;

public class FinishOffersHandler : IRequestHandler<FinishOffersCommand>
{
    private readonly IOfferRepository _offerRepository;
    private readonly IDateService _dateService;

    public FinishOffersHandler(IOfferRepository offerRepository, IDateService dateService)
    {
        _offerRepository = offerRepository;
        _dateService = dateService;
    }

    public async Task Handle(FinishOffersCommand request, CancellationToken cancellationToken)
    {
        var offers = await _offerRepository.GetAllAsync(cancellationToken);

        foreach (var offer in offers)
        {
            if (offer.DateTo < _dateService.CurrentOffsetDate())
            {
                offer.ChangeStatus(OfferStatus.Finished);
                await _offerRepository.UpdateAsync(offer, cancellationToken);
            }
        }
    }
}