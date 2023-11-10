using MediatR;
using skit.Core.Offers.Enums;

namespace skit.Application.Offers.Commands.UpdateOffer;

public sealed record UpdateOfferCommand(
    string Title,
    string? Description,
    DateTimeOffset? DateFrom,
    DateTimeOffset? DateTo,
    OfferStatus Status,
    OfferSeniority Seniority,
    OfferWorkLocation WorkLocation) : IRequest
{
    internal Guid OfferId { get; set; }
}