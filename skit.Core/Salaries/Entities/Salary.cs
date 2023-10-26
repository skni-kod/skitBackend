using skit.Core.Offers.Entities;
using skit.Core.Salaries.Enums;
using skit.Shared.Abstractions.Entities;

namespace skit.Core.Salaries.Entities;

public sealed class Salary : Entity
{
    public decimal? SalaryFrom { get; set; }
    public decimal? SalaryTo { get; set; }
    public SalaryEmploymentType? EmploymentType { get; set; }
    
    public Guid OfferId { get; set; }
    public Offer Offer { get; set; }
}