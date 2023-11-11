using System.Text.Json.Serialization;
using MediatR;

namespace skit.Application.Addresses.Commands.UpdateAddress;

public sealed record UpdateAddressCommand(
    string City,
    string Street,
    string? HouseNumber,
    string? PostalCode) : IRequest
{
    [JsonIgnore] public Guid Id { get; set; }
}