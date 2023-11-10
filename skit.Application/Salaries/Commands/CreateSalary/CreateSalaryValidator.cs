using FluentValidation;

namespace skit.Application.Salaries.Commands.CreateSalary;

public class CreateSalaryValidator : AbstractValidator<CreateSalaryCommand>
{
    public CreateSalaryValidator()
    {
        RuleFor(salary => salary.EmploymentType)
            .IsInEnum();

        RuleFor(salary => salary.SalaryFrom)
            .LessThan(salary => salary.SalaryTo)
            .When(salary => salary.SalaryFrom != null && salary.SalaryTo != null);
        
        RuleFor(salary => salary.SalaryTo)
            .GreaterThan(salary => salary.SalaryFrom)
            .When(salary => salary.SalaryFrom != null && salary.SalaryTo != null);
    }
}