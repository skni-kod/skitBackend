using MediatR;

namespace skit.Application.Addresses.Commands.CreateAddress;

public sealed record CreateAddressCommand(
    string City,
    string Street,
    string? HouseNumber,
    string? PostalCode) : IRequest;