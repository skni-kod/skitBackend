using MediatR;
using skit.Core.Offers.Enums;
using skit.Core.Salaries.Enums;

namespace skit.Application.Offers.Commands.UpdateOffer;

public sealed record UpdateOfferCommand(
    string Title,
    string? Description,
    DateTimeOffset? DateFrom,
    DateTimeOffset? DateTo,
    OfferStatus Status,
    OfferSeniority Seniority,
    OfferWorkLocation WorkLocation,
    List<UpdateOfferSalaries> Salaries) : IRequest
{
    internal Guid OfferId { get; set; }
}

public sealed record UpdateOfferSalaries(decimal SalaryFrom, decimal? SalaryTo, SalaryEmploymentType EmploymentType);