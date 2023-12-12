using FluentValidation;

namespace skit.Application.Companies.Commands.UpdateCompany;

public class UpdateCompanyValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyValidator()
    {
        RuleFor(command => command.Description)
            .MaximumLength(250);
        
        RuleFor(command => command.Size)
            .IsInEnum();

        RuleFor(command => command.Links)
            .MaximumLength(250);
    }
}