using MediatR;
using skit.Core.Offers.Enums;
using skit.Core.Salaries.Enums;
using skit.Shared.Models;

namespace skit.Application.Offers.Queries.BrowsePublicOffers;

public sealed record BrowsePublicOffersQuery(
    string? Search,
    List<OfferSeniority> OfferSeniorities,
    List<OfferWorkLocation> OfferWorkLocations,
    List<string> Cities,
    decimal SalaryFrom,
    List<SalaryEmploymentType> EmploymentType,
    List<Guid> TechnologyIds) : PaginationRequest, IRequest<BrowsePublicOffersResponse>;