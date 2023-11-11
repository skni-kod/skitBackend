using MediatR;

namespace skit.Application.Addresses.Queries.GetAddressForUpdate;

public sealed record GetAddressQuery(Guid Id) : IRequest<GetAddressResponse>;
