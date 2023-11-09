using skit.Application.Offers.Queries.DTO;
using skit.Shared.Abstractions.Queries;

namespace skit.Application.Offers.Queries.BrowseOffers.DTO;

public sealed record BrowseOffersDto(Paged<OfferDto> Offers);