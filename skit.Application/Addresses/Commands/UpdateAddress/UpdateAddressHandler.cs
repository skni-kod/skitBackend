using MediatR;
using skit.Core.Addresses.Exceptions;
using skit.Core.Addresses.Repositories;

namespace skit.Application.Addresses.Commands.UpdateAddress;

public sealed class UpdateAddressHandler : IRequestHandler<UpdateAddressCommand>
{
    private readonly IAddressRepository _addressRepository;

    public UpdateAddressHandler(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }
    
    public async Task Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetAsync(request.Id, cancellationToken)
                      ?? throw new AddressNotFoundException();

        address.Update(
            request.City,
            request.Street,
            request.HouseNumber,
            request.PostalCode);

        var result = await _addressRepository.UpdateAsync(address, cancellationToken);
    }
}