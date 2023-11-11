using FluentValidation;

namespace skit.Application.Addresses.Commands.CreateAddress;

public sealed class CreateAddressValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressValidator()
    {
        
    }
}