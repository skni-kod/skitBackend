using skit.Core.Companies.Entities;
using skit.Core.Offers.Entities;
using skit.Shared.Abstractions.Entities;

namespace skit.Core.Addresses.Entities;

public sealed class Address : Entity
{
    public string City { get; private set; }
    public string Street { get; private set; }
    public string? HouseNumber { get; private set; }
    public string? PostalCode { get; private set; }
    
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; }

    private List<Offer> _offers = new();
    public IReadOnlyCollection<Offer> Offers { get; }
    
    private Address() {}

    private Address(string city, string street, string? houseNumber, string? postalCode, Guid companyId)
    {
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        PostalCode = postalCode;
        CompanyId = companyId;
    }

    public static Address Create(string city, string street, string? houseNumber, string? postalCode, Guid companyId)
        => new(city, street, houseNumber, postalCode, companyId);
    
    public void Update(string city, string street, string? houseNumber, string? postalCode)
    {
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        PostalCode = postalCode;
    }


}