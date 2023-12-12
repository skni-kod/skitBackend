using MediatR;
using skit.Core.Addresses.Entities;
using skit.Core.Addresses.Exceptions;
using skit.Core.Addresses.Repositories;
using skit.Core.Common.Extensions;
using skit.Core.Common.Services;
using skit.Core.Offers.Exceptions;
using skit.Core.Offers.Repositories;
using skit.Core.Salaries.Entities;
using skit.Core.Salaries.Exceptions;
using skit.Core.Technologies.Entities;
using skit.Core.Technologies.Exceptions;
using skit.Core.Technologies.Repositories;
using skit.Shared.Responses;

namespace skit.Application.Offers.Commands.UpdateOffer;

internal sealed class UpdateOfferHandler : IRequestHandler<UpdateOfferCommand, CreateOrUpdateResponse>
{
    private readonly IOfferRepository _offerRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly ITechnologyRepository _technologyRepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdateOfferHandler(IOfferRepository offerRepository, IAddressRepository addressRepository, ICurrentUserService currentUserService, ITechnologyRepository technologyRepository)
    {
        _offerRepository = offerRepository;
        _addressRepository = addressRepository;
        _currentUserService = currentUserService;
        _technologyRepository = technologyRepository;
    }

    public async Task<CreateOrUpdateResponse> Handle(UpdateOfferCommand command, CancellationToken cancellationToken)
    {
        var offer = await _offerRepository.GetAsync(command.OfferId, cancellationToken);

        if (offer == null)
            throw new OfferNotFoundException();
        
        if (offer.CompanyId != _currentUserService.CompanyId)
            throw new OfferForbiddenException();
        
        var isSingleEmploymentTypes = command.Salaries
            .GroupBy(salary => salary.EmploymentType)
            .All(grp => grp.Count() == 1);

        if (!isSingleEmploymentTypes)
            throw new DuplicateEmploymentTypeException();

        var addresses = await _addressRepository.GetFromIdsListForCompanyAsync(command.AddressIds,
            _currentUserService.CompanyId, cancellationToken);

        var technologies = await _technologyRepository.GetFromIdsListAsync(command.TechnologyIds, cancellationToken);

        var salaries = new List<Salary>();
        
        foreach (var salary in command.Salaries)
        {
            salaries.Add(Salary.Create(salary.SalaryFrom, salary.SalaryTo, salary.EmploymentType));
        }

        var seniority = command.Seniorities.AggregateToFlag();
        var workLocations = command.WorkLocations.AggregateToFlag();
        
        offer.Update(
            command.Title,
            command.Description,
            command.DateFrom,
            command.DateTo,
            command.Status,
            seniority,
            workLocations,
            salaries,
            addresses,
            technologies
        );

        var offerId = await _offerRepository.UpdateAsync(offer, cancellationToken);
        
        return new CreateOrUpdateResponse(offerId);
    }
}