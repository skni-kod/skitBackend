using MediatR;
using skit.Core.Offers.Enums;
using skit.Core.Salaries.Enums;
using skit.Shared.Responses;

namespace skit.Application.Offers.Commands.UpdateOffer;

public sealed record UpdateOfferCommand(
    string Title,
    string? Description,
    DateTimeOffset? DateFrom,
    DateTimeOffset? DateTo,
    OfferStatus Status,
    List<OfferSeniority> Seniorities,
    List<OfferWorkLocation> WorkLocations,
    List<UpdateOfferSalaries> Salaries,
    List<Guid> AddressIds,
    List<Guid> TechnologyIds) : IRequest<CreateOrUpdateResponse>
{
    internal Guid OfferId { get; set; }
}

public sealed record UpdateOfferSalaries(decimal SalaryFrom, decimal? SalaryTo, SalaryEmploymentType EmploymentType);