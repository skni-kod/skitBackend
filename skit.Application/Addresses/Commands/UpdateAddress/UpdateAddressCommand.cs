using System.Text.Json.Serialization;
using MediatR;
using skit.Shared.Responses;

namespace skit.Application.Addresses.Commands.UpdateAddress;

public sealed record UpdateAddressCommand(
    string City,
    string Street,
    string? HouseNumber,
    string? PostalCode) : IRequest<CreateOrUpdateResponse>
{
    internal Guid Id { get; set; }
}