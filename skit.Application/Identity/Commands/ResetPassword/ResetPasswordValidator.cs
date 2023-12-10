using FluentValidation;

namespace skit.Application.Identity.Commands.ResetPassword;

public sealed class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        RuleFor(x => x.ConfirmedPassword).NotEmpty().WithMessage("Confirmed password is required");
        RuleFor(x => x.ConfirmedPassword).Equal(x => x.Password).WithMessage("Confirmed password must by the same");
    }
}