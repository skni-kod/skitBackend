using skit.Application.Addresses.Queries.DTO;

namespace skit.Application.Addresses.Queries.BrowseAddresses;

public sealed record BrowseAddressesResponse(List<AddressDto> Addresses);