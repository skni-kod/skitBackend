using MediatR;

namespace skit.Application.Offers.Queries.GetOfferForUpdate;

public sealed record GetOfferForUpdateQuery(Guid OfferId) : IRequest<GetOfferForUpdateResponse>;