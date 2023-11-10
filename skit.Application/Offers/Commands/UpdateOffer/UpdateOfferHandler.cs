using MediatR;
using skit.Core.Offers.Entities;
using skit.Core.Offers.Exceptions;
using skit.Core.Offers.Repositories;

namespace skit.Application.Offers.Commands.UpdateOffer;

internal sealed class UpdateOfferHandler : IRequestHandler<UpdateOfferCommand>
{
    private readonly IOfferRepository _offerRepository;

    public UpdateOfferHandler(IOfferRepository offerRepository)
    {
        _offerRepository = offerRepository;
    }

    public async Task Handle(UpdateOfferCommand command, CancellationToken cancellationToken)
    {
        var offer = await _offerRepository.GetAsync(command.OfferId, cancellationToken);

        if (offer == null)
            throw new OfferNotFoundException();
        
        offer.Update(
            command.Title,
            command.Description,
            command.DateFrom,
            command.DateTo,
            command.Status,
            command.Seniority,
            command.WorkLocation);
        
        await _offerRepository.UpdateAsync(offer, cancellationToken);
    }
}