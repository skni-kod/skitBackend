using skit.Core.Salaries.Enums;

namespace skit.Application.Offers.Queries.DTO;

public sealed class SalaryDto
{
    public decimal SalaryFrom { get; set; }
    public decimal? SalaryTo { get; set; }
    public SalaryEmploymentType EmploymentType { get; set; }
}