using skit.Application.Addresses.Queries.DTO;
using skit.Core.Addresses.Entities;

namespace skit.Infrastructure.DAL.Addresses.Queries;

public static class Extensions
{
    public static AddressDto AsDto(this Address? address)
    {
        return new()
        {
            Id = address.Id,
            City = address.City,
            Street = address.Street,
            HouseNumber = address.HouseNumber,
            PostalCode = address.PostalCode
        };
    }
}