using FluentValidation;
using skit.Application.Salaries.Commands.CreateSalariesFromList;

namespace skit.Application.Offers.Commands.CreateOffer;

public class CreateOfferValidator : AbstractValidator<CreateOfferCommand>
{
    public CreateOfferValidator()
    {
        RuleFor(command => command.Title)
            .NotEmpty();

        RuleFor(command => command.DateFrom)
            .LessThan(command => command.DateTo)
            .When(command => command.DateFrom != null && command.DateTo != null);
        
        RuleFor(command => command.DateTo)
            .GreaterThan(command => command.DateFrom)
            .When(command => command.DateFrom != null && command.DateTo != null);

        RuleFor(command => command.Status)
            .IsInEnum();
        
        RuleFor(command => command.Seniority)
            .IsInEnum();
        
        RuleFor(command => command.WorkLocation)
            .IsInEnum();

        RuleFor(command => command.Salaries)
            .SetValidator(new CreateSalariesFromListValidator());
    }
}