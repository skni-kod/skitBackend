namespace skit.Application.Addresses.Queries.DTO;

public sealed class AddressDto
{
    public Guid Id { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? PostalCode { get; set; }
}