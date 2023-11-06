using FluentValidation;

namespace skit.Application.Identity.Commands.SignUpCompany;

public sealed class SignUpCompanyValidator : AbstractValidator<SignUpCompanyCommand>
{
    public SignUpCompanyValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Invalid email address");

        RuleFor(x => x.CompanyName)
            .MaximumLength(250);

        RuleFor(x => x.Password)
            .Equal(x => x.ConfirmedPassword);
    }
}