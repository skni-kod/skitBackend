using MediatR;

namespace skit.Application.Addresses.Queries.BrowseAddresses;

public sealed record BrowseAddressesQuery(string? Search) : IRequest<BrowseAddressesResponse>;
