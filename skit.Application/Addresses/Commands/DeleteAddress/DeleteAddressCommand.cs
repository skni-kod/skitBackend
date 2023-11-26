using MediatR;

namespace skit.Application.Addresses.Commands.DeleteAddress;

public sealed record DeleteAddressCommand(Guid Id) : IRequest;
