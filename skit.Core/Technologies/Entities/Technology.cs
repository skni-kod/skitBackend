using skit.Core.Offers.Entities;
using skit.Shared.Abstractions.Entities;

namespace skit.Core.Technologies.Entities;

public sealed class Technology : Entity
{
    public string Name { get; private set; }
    public string? ThumUrl { get; private set; }
    
    private List<Offer> _offers = new();
    public IReadOnlyCollection<Offer> Offers => _offers;
    
    private Technology()
    {
    }
    
    private Technology(string name, string? thumUrl)
    {
        Name = name;
        ThumUrl = thumUrl;
    }

    public static Technology Create(string name, string? thumUrl)
        => new(name, thumUrl);

    public void Update(string name, string? thumUrl)
    {
        Name = name;
        ThumUrl = thumUrl;
    }
}