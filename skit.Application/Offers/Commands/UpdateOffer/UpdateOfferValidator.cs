using FluentValidation;

namespace skit.Application.Offers.Commands.UpdateOffer;

public class UpdateOfferValidator : AbstractValidator<UpdateOfferCommand>
{
    public UpdateOfferValidator()
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
    }
}