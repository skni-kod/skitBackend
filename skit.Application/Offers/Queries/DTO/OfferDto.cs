using skit.Core.Addresses.Entities;
using skit.Core.Common.DTO;
using skit.Core.Offers.Enums;
using skit.Core.Salaries.Enums;

namespace skit.Application.Offers.Queries.DTO;

public sealed class OfferDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string CompanyName { get; set; }
    public List<BaseEnum?> WorkLocation { get; set; }
    public List<string> Cities { get; set; }
    public List<SalaryDto> Salaries { get; set; }
    public List<TechnologyDto> Technologies { get; set; }
}
