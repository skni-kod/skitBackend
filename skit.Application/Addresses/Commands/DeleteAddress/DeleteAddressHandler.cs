using MediatR;
using skit.Core.Addresses.Exceptions;
using skit.Core.Addresses.Repositories;

namespace skit.Application.Addresses.Commands.DeleteAddress;

public sealed class DeleteAddressHandler : IRequestHandler<DeleteAddressCommand>
{
    private readonly IAddressRepository _addressRepository;

    public DeleteAddressHandler(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }
    
    public async Task Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetAsync(request.Id, cancellationToken)
                      ?? throw new AddressNotFoundException();

        await _addressRepository.DeleteAsync(address, cancellationToken);
    }
}