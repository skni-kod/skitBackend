using MediatR;
using skit.Core.Addresses.Entities;
using skit.Core.Addresses.Repositories;
using skit.Core.Common.Services;

namespace skit.Application.Addresses.Commands.CreateAddress;

public sealed class CreateAddressHandler : IRequestHandler<CreateAddressCommand>
{
    private readonly IAddressRepository _addressRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateAddressHandler(IAddressRepository addressRepository, ICurrentUserService currentUserService)
    {
        _addressRepository = addressRepository;
        _currentUserService = currentUserService;
    }
    
    public async Task Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var address = Address.Create(
            request.City,
            request.Street,
            request.HouseNumber,
            request.PostalCode,
            _currentUserService.CompanyId);

        var result = await _addressRepository.AddAsync(address, cancellationToken);
    }
}