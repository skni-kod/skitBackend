using MediatR;
using skit.Core.Addresses.Exceptions;
using skit.Core.Addresses.Repositories;
using skit.Core.Common.Extensions;
using skit.Core.Common.Services;
using skit.Core.Offers.Entities;
using skit.Core.Offers.Repositories;
using skit.Core.Salaries.Entities;
using skit.Core.Salaries.Exceptions;
using skit.Shared.Responses;

namespace skit.Application.Offers.Commands.CreateOffer;

internal sealed class CreateOfferHandler : IRequestHandler<CreateOfferCommand, CreateOrUpdateResponse>
{
    private readonly IOfferRepository _offerRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateOfferHandler(IOfferRepository offerRepository, IAddressRepository addressRepository, ICurrentUserService currentUserService)
    {
        _offerRepository = offerRepository;
        _addressRepository = addressRepository;
        _currentUserService = currentUserService;
    }

    public async Task<CreateOrUpdateResponse> Handle(CreateOfferCommand command, CancellationToken cancellationToken)
    {
        if(command.Salaries.Any())
        {
            var isSingleEmploymentTypes = command.Salaries
                .GroupBy(salary => salary.EmploymentType)
                .All(grp => grp.Count() == 1);

            if (!isSingleEmploymentTypes)
                throw new DuplicateEmploymentTypeException();
        }

        var addresses = await _addressRepository.GetFromIdsListForCompanyAsync(command.AddressIds,
            _currentUserService.CompanyId, cancellationToken);

        if (addresses.Count != command.AddressIds.Count)
            throw new AddressNotFoundException();
        
        var salaries = new List<Salary>();
        
        foreach (var salary in command.Salaries)
        {
            salaries.Add(Salary.Create(salary.SalaryFrom, salary.SalaryTo, salary.EmploymentType));
        }
        
        var seniority = command.Seniorities.AggregateToFlag();
        var workLocations = command.WorkLocations.AggregateToFlag();
        
        var offer = Offer.Create(
            command.Title,
            command.Description,
            command.DateFrom,
            command.DateTo,
            command.Status,
            seniority,
            workLocations,
            _currentUserService.CompanyId,
            salaries,
            addresses);

        await _offerRepository.AddAsync(offer, cancellationToken);
        
        return new CreateOrUpdateResponse(offer.Id);
    }
}