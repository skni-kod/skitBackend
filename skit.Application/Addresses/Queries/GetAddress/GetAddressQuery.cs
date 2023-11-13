using MediatR;

namespace skit.Application.Addresses.Queries.GetAddress;

public sealed record GetAddressQuery(Guid Id) : IRequest<GetAddressForUpdate.GetAddressResponse>;
