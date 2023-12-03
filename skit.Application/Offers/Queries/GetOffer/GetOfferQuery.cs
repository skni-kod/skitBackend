using MediatR;

namespace skit.Application.Offers.Queries.GetOffer;

public sealed record GetOfferQuery(Guid OfferId) : IRequest<GetOfferResponse>;