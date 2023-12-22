using skit.Core.Files.Entities;
using skit.Core.Offers.Entities;
using skit.Shared.Abstractions.Entities;

namespace skit.Core.Technologies.Entities;

public sealed class Technology : Entity
{
    public string Name { get; private set; }
    public string? ThumUrl { get; private set; }
    
    public Guid? PhotoId { get; set; }
    public SystemFile Photo { get; set; }
    
    private List<Offer> _offers = new();
    public IReadOnlyCollection<Offer> Offers => _offers;
    
    private Technology()
    {
    }
    
    private Technology(string name, string? thumUrl, Guid? photoId)
    {
        Name = name;
        ThumUrl = thumUrl;
        PhotoId = photoId;
    }

    public static Technology Create(string name, string? thumUrl, Guid? photoId)
        => new(name, thumUrl, photoId);

    public void Update(string name, string? thumUrl)
    {
        Name = name;
        ThumUrl = thumUrl;
    }
}