using System.Net.Mime;
using skit.Core.Addresses.Entities;
using skit.Core.Companies.Entities;
using skit.Core.JobApplications.Entities;
using skit.Core.Offers.Enums;
using skit.Core.Salaries.Entities;
using skit.Shared.Abstractions.Entities;

namespace skit.Core.Offers.Entities;

public sealed class Offer : Entity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset? DateFrom { get; set; }
    public DateTimeOffset? DateTo { get; set; }
    public OfferStatus Status { get; set; }
    public OfferSeniority Seniority { get; set; }
    public OfferWorkLocation WorkLocation { get; set; }
    
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    
    public List<Address> Addresses { get; set; }
    public List<Salary> Salaries { get; set; }
    public List<JobApplication> JobApplications { get; set; }
}