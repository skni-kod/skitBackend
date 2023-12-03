using skit.Application.Offers.Queries.DTO;
using skit.Shared.Abstractions.Models;

namespace skit.Application.Offers.Queries.BrowseOffers;

public sealed record BrowseOffersResponse(PaginatedList<OfferDto> Offers);