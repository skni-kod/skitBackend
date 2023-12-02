using FluentValidation;
using skit.Core.Offers.Enums;

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

        RuleFor(command => command.Seniorities)
            .NotEmpty();

        RuleForEach(command => command.Seniorities)
            .NotEmpty()
            .Custom((seniority, ctx) =>
            {
                if (!Enum.IsDefined(typeof(OfferSeniority), seniority))
                {
                    ctx.AddFailure($"Invalid seniority value: {seniority}");
                }
            });

        RuleFor(command => command.WorkLocations)
            .NotEmpty();

        RuleForEach(command => command.WorkLocations)
            .NotEmpty()
            .Custom((workLocation, ctx) =>
            {
                if (!Enum.IsDefined(typeof(OfferWorkLocation), workLocation))
                {
                    ctx.AddFailure($"Invalid work location value: {workLocation}");
                }
            });

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
    }
}