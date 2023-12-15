using MediatR;
using skit.Shared.Models;

namespace skit.Application.Offers.Queries.BrowseOffers;

public sealed record BrowseOffersQuery(string? Search) : PaginationRequest, IRequest<BrowseOffersResponse>;