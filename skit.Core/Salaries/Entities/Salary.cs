using skit.Core.Offers.Entities;
using skit.Core.Salaries.Enums;
using skit.Shared.Abstractions.Entities;

namespace skit.Core.Salaries.Entities;

public sealed class Salary : Entity
{
    public decimal? SalaryFrom { get; private set; }
    public decimal? SalaryTo { get; private set; }
    public SalaryEmploymentType? EmploymentType { get; private set; }
    
    public Guid OfferId { get; private set; }
    public Offer Offer { get; private set; }

    private Salary()
    {
        
    }

    private Salary(decimal? salaryFrom, decimal? salaryTo, SalaryEmploymentType? employmentType, Guid offerId)
    {
        SalaryFrom = salaryFrom;
        SalaryTo = salaryTo;
        EmploymentType = employmentType;
        OfferId = offerId;
    }

    public static Salary Create(decimal? salaryFrom, decimal? salaryTo, SalaryEmploymentType? employmentType, Guid offerId)
        => new(salaryFrom, salaryTo, employmentType, offerId);

    public void Update(decimal? salaryFrom, decimal? salaryTo)
    {
        SalaryFrom = salaryFrom;
        SalaryTo = salaryTo;
    }
}