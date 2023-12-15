using skit.Application.Offers.Queries.DTO;
using skit.Shared.Models;

namespace skit.Application.Offers.Queries.BrowsePublicOffers;

public sealed record BrowsePublicOffersResponse(PaginatedList<OfferDto> Offers);