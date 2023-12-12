using skit.Application.Offers.Commands.UpdateOffer;
using skit.Core.Offers.Enums;

namespace skit.Application.Offers.Queries.GetOfferForUpdate.DTO;

public sealed class GetOfferForUpdateDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset? DateFrom { get; set; }
    public DateTimeOffset? DateTo { get; set; }
    public OfferStatus? Status { get; set; }
    public List<OfferSeniority> Seniorities { get; set; }
    public List<OfferWorkLocation> WorkLocations { get; set; }
    public List<UpdateOfferSalaries?> Salaries { get; set; }
    public List<Guid> AddressIds { get; set; }
    public List<Guid> TechnologyIds { get; set; }
}