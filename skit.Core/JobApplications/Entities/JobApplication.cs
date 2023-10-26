using skit.Core.Offers.Entities;
using skit.Shared.Abstractions.Entities;

namespace skit.Core.JobApplications.Entities;

public sealed class JobApplication : Entity
{
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public string? Description { get; set; }
    
    public Guid OfferId { get; set; }
    public Offer Offer { get; set; }
}