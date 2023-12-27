using skit.Core.Offers.Entities;
using skit.Shared.Abstractions.Entities;

namespace skit.Core.JobApplications.Entities;

public sealed class JobApplication : Entity
{
    public string FirstName { get; private set; }
    public string SurName { get; private set; }
    public string? Description { get; private set; }
    
    public Guid OfferId { get; private set; }
    public Offer Offer { get; private set; }

    private JobApplication() {}

    private JobApplication(string firstName, string surName, string? description, Guid offerId)
    {
        FirstName = firstName;
        SurName = surName;
        Description = description;
        OfferId = offerId;
    }

    public static JobApplication Create(string firstName, string surName, string? description, Guid offerId)
        => new(firstName, surName, description, offerId);
}