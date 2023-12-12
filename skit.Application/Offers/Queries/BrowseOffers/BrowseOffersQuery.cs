using MediatR;
using skit.Shared.Abstractions.Models;

namespace skit.Application.Offers.Queries.BrowseOffers;

public sealed record BrowseOffersQuery(string? Search) : PaginationRequest, IRequest<BrowseOffersResponse>;