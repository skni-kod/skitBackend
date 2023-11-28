using MediatR;
using skit.Core.Offers.Exceptions;
using skit.Core.Offers.Repositories;

namespace skit.Application.Offers.Commands.DeleteOffer;

internal sealed class DeleteOfferHandler : IRequestHandler<DeleteOfferCommand>
{
    private readonly IOfferRepository _offerRepository;

    public DeleteOfferHandler(IOfferRepository offerRepository)
    {
        _offerRepository = offerRepository;
    }

    public async Task Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
    {
        var offer = await _offerRepository.GetAsync(request.OfferId, cancellationToken)
                    ?? throw new OfferNotFoundException();

        await _offerRepository.DeleteAsync(offer, cancellationToken);
    }
}