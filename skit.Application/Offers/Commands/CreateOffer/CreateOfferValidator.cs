using FluentValidation;

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
        
        RuleFor(command => command.Seniority)
            .IsInEnum();
        
        RuleFor(command => command.WorkLocation)
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
            .Must(collection => collection.Count > 0);

        RuleForEach(command => command.AddressIds)
            .NotEmpty();
    }
}