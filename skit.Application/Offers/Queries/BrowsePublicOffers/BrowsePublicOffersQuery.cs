using MediatR;
using skit.Shared.Abstractions.Models;

namespace skit.Application.Offers.Queries.BrowsePublicOffers;

public sealed record BrowsePublicOffersQuery(string? Search) : PaginationRequest, IRequest<BrowsePublicOffersResponse>;