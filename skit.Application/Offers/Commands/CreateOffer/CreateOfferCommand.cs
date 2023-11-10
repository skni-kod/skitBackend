using MediatR;
using skit.Application.Salaries.Commands.CreateSalariesFromList;
using skit.Core.Offers.Enums;

namespace skit.Application.Offers.Commands.CreateOffer;

public sealed class CreateOfferCommand : IRequest<CreateOfferResponse>
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset? DateFrom { get; set; }
    public DateTimeOffset? DateTo { get; set; }
    public OfferStatus Status { get; set; }
    public OfferSeniority Seniority { get; set; }
    public OfferWorkLocation WorkLocation { get; set; }
    public Guid CompanyId { get; set; }
    public CreateSalariesFromListCommand Salaries { get; set; }
}