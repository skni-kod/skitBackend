using FluentValidation;
using skit.Shared.Regexes;

namespace skit.Application.Addresses.Commands.UpdateAddress;

public sealed class UpdateAddressValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressValidator()
    {
        RuleFor(x => x.PostalCode)
            .Matches(GlobalRegex.PostalCodeRegex)
            .WithMessage("Invalid postal code");
    }
}