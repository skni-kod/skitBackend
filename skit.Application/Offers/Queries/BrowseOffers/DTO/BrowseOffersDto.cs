using skit.Application.Offers.Queries.DTO;
using skit.Shared.Abstractions.Models;

namespace skit.Application.Offers.Queries.BrowseOffers.DTO;

public sealed record BrowseOffersDto(PaginatedList<OfferDto> Offers);