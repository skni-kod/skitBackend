using skit.Core.Companies.Entities;
using skit.Core.Offers.Entities;
using skit.Shared.Abstractions.Entities;

namespace skit.Core.Addresses.Entities;

public sealed class Address : Entity
{
    public string City { get; set; }
    public string Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? PostalCode { get; set; }
    
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    
    public List<Offer> Offers { get; set; }
}