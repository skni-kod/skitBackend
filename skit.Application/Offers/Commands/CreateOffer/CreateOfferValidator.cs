using FluentValidation;
using skit.Core.Offers.Enums;

namespace skit.Application.Offers.Commands.CreateOffer;

public class CreateOfferValidator : AbstractValidator<CreateOfferCommand>
{
    public CreateOfferValidator()
    {
        RuleFor(command => command.Title)
            .NotEmpty();

        RuleFor(command => command.DateTo)
            .GreaterThan(command => command.DateFrom)
            .When(command => command.DateFrom != null && command.DateTo != null);

        RuleFor(command => command.Status)
            .IsInEnum();

        RuleFor(command => command.Seniorities)
            .NotEmpty();

        RuleForEach(command => command.Seniorities)
            .NotEmpty()
            .IsInEnum();

        RuleFor(command => command.WorkLocations)
            .NotEmpty();

        RuleForEach(command => command.WorkLocations)
            .NotEmpty()
            .IsInEnum();

        RuleForEach(command => command.Salaries).ChildRules(salary =>
        {
            salary.RuleFor(s => s.EmploymentType)
                .IsInEnum();

            salary.RuleFor(s => s.SalaryFrom)
                .LessThan(s => s.SalaryTo)
                .When(s => s.SalaryTo != null);
        });

        RuleFor(command => command.AddressIds)
            .NotEmpty();
        
        RuleFor(command => command.TechnologyIds)
            .NotEmpty();
    }
}