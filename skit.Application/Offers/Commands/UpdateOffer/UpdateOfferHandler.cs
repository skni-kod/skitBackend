using MediatR;
using skit.Core.Offers.Entities;
using skit.Core.Offers.Exceptions;
using skit.Core.Offers.Repositories;

namespace skit.Application.Offers.Commands.UpdateOffer;

internal sealed class UpdateOfferHandler : IRequestHandler<UpdateOfferCommand>
{
    private IMediator _mediator;
    private readonly IOfferRepository _offerRepository;

    public UpdateOfferHandler(IOfferRepository offerRepository, IMediator mediator)
    {
        _offerRepository = offerRepository;
        _mediator = mediator;
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

        command.Salaries.OfferId = command.OfferId;
        
        await _mediator.Send(command.Salaries, cancellationToken);
    }
}