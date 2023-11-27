using MediatR;
using skit.Core.Offers.Enums;
using skit.Core.Salaries.Enums;
using skit.Shared.Responses;

namespace skit.Application.Offers.Commands.CreateOffer;

public sealed class CreateOfferCommand : IRequest<CreateOrUpdateResponse>
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset? DateFrom { get; set; }
    public DateTimeOffset? DateTo { get; set; }
    public OfferStatus Status { get; set; }
    public OfferSeniority Seniority { get; set; }
    public OfferWorkLocation WorkLocation { get; set; }
    public List<CreateOfferSalaries> Salaries { get; set; } = new();
    public List<Guid> AddressIds { get; set; } = new();
}

public sealed record CreateOfferSalaries(decimal SalaryFrom, decimal? SalaryTo, SalaryEmploymentType EmploymentType);