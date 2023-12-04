using MediatR;
using skit.Core.Common.Services;
using skit.Core.Offers.Exceptions;
using skit.Core.Offers.Repositories;

namespace skit.Application.Offers.Commands.DeleteOffer;

internal sealed class DeleteOfferHandler : IRequestHandler<DeleteOfferCommand>
{
    private readonly IOfferRepository _offerRepository;
    private readonly ICurrentUserService _currentUserService;

    public DeleteOfferHandler(IOfferRepository offerRepository, ICurrentUserService currentUserService)
    {
        _offerRepository = offerRepository;
        _currentUserService = currentUserService;
    }

    public async Task Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
    {
        var offer = await _offerRepository.GetAsync(request.OfferId, cancellationToken)
                    ?? throw new OfferNotFoundException();

        if (offer.CompanyId != _currentUserService.CompanyId)
            throw new OfferForbiddenException();

        await _offerRepository.DeleteAsync(offer, cancellationToken);
    }
}