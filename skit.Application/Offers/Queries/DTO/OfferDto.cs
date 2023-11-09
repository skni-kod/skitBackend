using skit.Core.Addresses.Entities;
using skit.Core.Offers.Enums;

namespace skit.Application.Offers.Queries.DTO;

public sealed class OfferDto
{
    public string Title { get; set; }
    public string CompanyName { get; set; }
    public OfferWorkLocation WorkLocation { get; set; }
    public List<string> Cities { get; set; }
    public decimal? SalaryFrom { get; set; }
    public decimal? SalaryTo { get; set; }
}