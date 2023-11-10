using FluentValidation;
using skit.Application.Salaries.Commands.CreateSalary;

namespace skit.Application.Salaries.Commands.CreateSalariesFromList;

public class CreateSalariesFromListValidator : AbstractValidator<CreateSalariesFromListCommand>
{
    public CreateSalariesFromListValidator()
    {
        RuleForEach(command => command.Salaries)
            .SetValidator(new CreateSalaryValidator());
    }
}