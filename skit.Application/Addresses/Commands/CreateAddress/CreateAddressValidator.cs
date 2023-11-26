using FluentValidation;
using skit.Shared.Regexes;

namespace skit.Application.Addresses.Commands.CreateAddress;

public sealed class CreateAddressValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressValidator()
    {
        RuleFor(x => x.PostalCode)
            .Matches(GlobalRegex.PostalCodeRegex)
            .WithMessage("Invalid postal code");
    }
}