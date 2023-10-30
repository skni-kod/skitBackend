using skit.Core.Addresses.Entities;
using skit.Core.Companies.Enums;
using skit.Core.Identity.Entities;
using skit.Core.Offers.Entities;
using skit.Shared.Abstractions.Entities;

namespace skit.Core.Companies.Entities;

public sealed class Company : Entity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public CompanySize Size { get; set; }
    public string? Links { get; set; }
    
    public Guid OwnerId { get; set; }
    public User Owner { get; set; }
    
    public List<Address> Addresses { get; set; }
    public List<Offer> Offers { get; set; }

    public void ChangeCompanyInformation(string? description, CompanySize size, string? links, Guid ownerId)
    {
        Description = description;
        Size = size;
        Links = links;
        OwnerId = ownerId;
    }
    
    public void SoftDelete()
    {
        DeletedById = Guid.Empty;
        DeletedAt = DateTimeOffset.Now.ToUniversalTime();
    }
}