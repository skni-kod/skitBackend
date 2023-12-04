using MediatR;

namespace skit.Application.Offers.Queries.GetPublicOffer;

public sealed record GetPublicOfferQuery(Guid OfferId) : IRequest<GetPublicOfferResponse>;
