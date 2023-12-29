using FluentValidation;

namespace skit.Application.Identity.Commands.SignUpCompany;

public sealed class SignUpCompanyValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCompanyValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Invalid email address");

        RuleFor(x => x.Password)
            .Equal(x => x.ConfirmedPassword);
    }
}