using MediatR;
using skit.Core.Addresses.Exceptions;
using skit.Core.Addresses.Repositories;
using skit.Core.Common.Services;
using skit.Core.Offers.Exceptions;
using skit.Core.Offers.Repositories;
using skit.Core.Salaries.Entities;
using skit.Core.Salaries.Exceptions;

namespace skit.Application.Offers.Commands.UpdateOffer;

internal sealed class UpdateOfferHandler : IRequestHandler<UpdateOfferCommand>
{
    private readonly IOfferRepository _offerRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdateOfferHandler(IOfferRepository offerRepository, IAddressRepository addressRepository, ICurrentUserService currentUserService)
    {
        _offerRepository = offerRepository;
        _addressRepository = addressRepository;
        _currentUserService = currentUserService;
    }

    public async Task Handle(UpdateOfferCommand command, CancellationToken cancellationToken)
    {
        var offer = await _offerRepository.GetAsync(command.OfferId, cancellationToken);

        if (offer == null)
            throw new OfferNotFoundException();
        
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

        offer.Update(
            command.Title,
            command.Description,
            command.DateFrom,
            command.DateTo,
            command.Status,
            command.Seniority,
            command.WorkLocation,
            salaries,
            addresses
        );

        await _offerRepository.UpdateAsync(offer, cancellationToken);
    }
}