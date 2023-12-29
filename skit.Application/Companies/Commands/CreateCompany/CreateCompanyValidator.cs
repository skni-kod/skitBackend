using FluentValidation;

namespace skit.Application.Companies.Commands.CreateCompany;

public sealed class CreateCompanyValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyValidator()
    {
        RuleFor(command => command.Description)
            .MaximumLength(250);
        
        RuleFor(command => command.Size)
            .IsInEnum();

        RuleFor(command => command.Links)
            .MaximumLength(250);
    }
}