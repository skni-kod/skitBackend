using MediatR;
using skit.Application.Offers.Queries.BrowsePublicOffers;
using skit.Core.Common.Extensions;
using skit.Core.Offers.Enums;
using skit.Infrastructure.DAL.EF.Context;
using skit.Shared.Extensions;

namespace skit.Infrastructure.DAL.Offers.Queries.BrowsePublicOffers;

internal sealed class BrowsePublicOffers : IRequestHandler<BrowsePublicOffersQuery, BrowsePublicOffersResponse>
{
    private readonly EFContext _context;

    public BrowsePublicOffers(EFContext context)
    {
        _context = context;
    }

    public async Task<BrowsePublicOffersResponse> Handle(BrowsePublicOffersQuery query, CancellationToken cancellationToken)
    {
        var offers = _context.Offers.AsNoTracking().Where(offer => offer.Status == OfferStatus.Public);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var searchTxt = $"%{query.Search}%";

            offers = offers.Where(offer => Microsoft.EntityFrameworkCore.EF.Functions.ILike(offer.Title, searchTxt) || 
                                           offer.Description != null && Microsoft.EntityFrameworkCore.EF.Functions.ILike(offer.Description, searchTxt));
        }

        if (query.Seniorities is not null && query.Seniorities.Any())
        {
            var querySeniorities = query.Seniorities.AggregateToFlag();
            offers = offers.Where(offer => (offer.Seniority & querySeniorities) != 0);
        }

        if (query.WorkLocations is not null && query.WorkLocations.Any())
        {
            var queryWorkLocations = query.WorkLocations.AggregateToFlag();
            offers = offers.Where(offer => (offer.WorkLocation & queryWorkLocations) != 0);
        }
            
        if (query.Cities is not null && query.Cities.Any(city => !string.IsNullOrWhiteSpace(city)))
            offers = offers.Where(offer => offer.Addresses.Any(address => query.Cities.Contains(address.City)));

        if (query.SalaryFrom is not null)
            offers = offers.Where(offer => offer.Salaries.Any(salary => salary.SalaryTo >= query.SalaryFrom));

        if (query.EmploymentType is not null && query.EmploymentType.Any())
            offers = offers.Where(offer => offer.Salaries.Any(salary => query.EmploymentType.Contains(salary.EmploymentType)));

        if (query.TechnologyIds is not null && query.TechnologyIds.Any())
            offers = offers.Where(offer => offer.Technologies.Any(technology => query.TechnologyIds.Contains(technology.Id)));


        var result = await offers
            .Include(offer => offer.Addresses)
            .Include(offer => offer.Salaries)
            .Include(offer => offer.Company)
            .Include(offer => offer.Technologies)
            .Select(offer => offer.AsDto())
            .ToPaginatedListAsync(query, cancellationToken);

        return new BrowsePublicOffersResponse(result);
    }
}