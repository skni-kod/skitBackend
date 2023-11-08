using skit.Core.Addresses.Entities;
using skit.Core.Companies.Enums;
using skit.Core.Identity.Entities;
using skit.Core.Offers.Entities;
using skit.Shared.Abstractions.Entities;

namespace skit.Core.Companies.Entities;

public sealed class Company : Entity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public CompanySize Size { get; private set; }
    public string? Links { get; private set; }

    public Guid OwnerId { get; private set; }
    public User Owner { get; set; }

    public List<Address> Addresses { get; }
    public List<Offer> Offers { get; }

    private Company()
    {
    }
    
    private Company(string name, string? description, CompanySize size, string? links, Guid ownerId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Size = size;
        Links = links;
        OwnerId = ownerId;
    }

    public void Update(string name, string? description, CompanySize size, string? links)
    {
        Name = name;
        Description = description;
        Size = size;
        Links = links;
    }
}