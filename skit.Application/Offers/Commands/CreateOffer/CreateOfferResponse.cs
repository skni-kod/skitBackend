
namespace skit.Application.Offers.Commands.CreateOffer;

public sealed record CreateOfferResponse(Guid Id, List<Guid> SalaryIds);