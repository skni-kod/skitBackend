using skit.Core.Addresses.Entities;
using skit.Core.Companies.Entities;
using skit.Core.JobApplications.Entities;
using skit.Core.Offers.Enums;
using skit.Core.Salaries.Entities;
using skit.Core.Technologies.Entities;
using skit.Shared.Abstractions.Entities;

namespace skit.Core.Offers.Entities;

public sealed class Offer : Entity
{
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DateTimeOffset? DateFrom { get; private set; }
    public DateTimeOffset? DateTo { get; private set; }
    public OfferStatus Status { get; private set; }
    public OfferSeniority Seniority { get; private set; }
    public OfferWorkLocation WorkLocation { get; private set; }
    
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; }
    
    private List<Address> _addresses = new();
    public IReadOnlyCollection<Address> Addresses => _addresses;
    private List<Salary> _salaries = new();
    public IReadOnlyCollection<Salary> Salaries => _salaries;
    private List<JobApplication> _jobApplications = new();
    public IReadOnlyCollection<JobApplication> JobApplications => _jobApplications;
    private List<Technology> _technologies = new();
    public IReadOnlyCollection<Technology> Technologies => _technologies;

    private Offer()
    {
    }

    private Offer(
        string title, 
        string? description, 
        DateTimeOffset? dateFrom, 
        DateTimeOffset? dateTo, 
        OfferStatus status,
        OfferSeniority seniority,
        OfferWorkLocation workLocation,
        Guid companyId,
        List<Salary> salaries,
        List<Address> addresses,
        List<Technology> technologies)
    {
        Title = title;
        Description = description;
        DateFrom = dateFrom;
        DateTo = dateTo;
        Status = status;
        Seniority = seniority;
        WorkLocation = workLocation;
        CompanyId = companyId;
        _salaries = salaries;
        _addresses = addresses;
        _technologies = technologies;
    }

    public static Offer Create(
        string title,
        string? description,
        DateTimeOffset? dateFrom,
        DateTimeOffset? dateTo,
        OfferStatus status,
        OfferSeniority seniority,
        OfferWorkLocation workLocation,
        Guid companyId,
        List<Salary> salaries,
        List<Address> addresses,
        List<Technology> technologies) =>
        new(title, description, dateFrom, dateTo, status, seniority, workLocation, companyId, salaries, addresses, technologies);

    public void Update(
        string title,
        string? description,
        DateTimeOffset? dateFrom,
        DateTimeOffset? dateTo,
        OfferStatus status,
        OfferSeniority seniority,
        OfferWorkLocation workLocation,
        List<Salary> salaries,
        List<Address> addresses,
        List<Technology> technologies)
    {
        Title = title;
        Description = description;
        DateFrom = dateFrom;
        DateTo = dateTo;
        Status = status;
        Seniority = seniority;
        WorkLocation = workLocation;
        _salaries = salaries;
        _addresses = addresses;
        _technologies = technologies;
    }
}