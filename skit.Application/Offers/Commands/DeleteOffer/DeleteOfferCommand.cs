using MediatR;

namespace skit.Application.Offers.Commands.DeleteOffer;

public sealed record DeleteOfferCommand(Guid OfferId) : IRequest;