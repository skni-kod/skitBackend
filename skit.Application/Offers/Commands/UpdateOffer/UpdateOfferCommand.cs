using MediatR;
using skit.Application.Salaries.Commands.UpdateSalariesFromList;
using skit.Core.Offers.Enums;

namespace skit.Application.Offers.Commands.UpdateOffer;

public sealed record UpdateOfferCommand(
    string Title,
    string? Description,
    DateTimeOffset? DateFrom,
    DateTimeOffset? DateTo,
    OfferStatus Status,
    OfferSeniority Seniority,
    OfferWorkLocation WorkLocation,
    UpdateSalariesFromListCommand Salaries) : IRequest
{
    internal Guid OfferId { get; set; }
}