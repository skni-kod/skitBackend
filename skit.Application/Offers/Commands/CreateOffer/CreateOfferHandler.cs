using MediatR;
using skit.Core.Offers.Entities;
using skit.Core.Offers.Repositories;

namespace skit.Application.Offers.Commands.CreateOffer;

internal sealed class CreateOfferHandler : IRequestHandler<CreateOfferCommand, CreateOfferResponse>
{
    private readonly IOfferRepository _offerRepository;
    private IMediator _mediator;

    public CreateOfferHandler(IOfferRepository offerRepository, IMediator mediator)
    {
        _offerRepository = offerRepository;
        _mediator = mediator;
    }

    public async Task<CreateOfferResponse> Handle(CreateOfferCommand command, CancellationToken cancellationToken)
    {
        var offer = Offer.Create(
            command.Title,
            command.Description,
            command.DateFrom,
            command.DateTo,
            command.Status,
            command.Seniority,
            command.WorkLocation,
            command.CompanyId);

        await _offerRepository.AddAsync(offer, cancellationToken);

        command.Salaries.OfferId = offer.Id;
        
        var createSalariesFromListResponse = await _mediator.Send(command.Salaries, cancellationToken);

        return new CreateOfferResponse(offer.Id, createSalariesFromListResponse.Ids);
    }
}