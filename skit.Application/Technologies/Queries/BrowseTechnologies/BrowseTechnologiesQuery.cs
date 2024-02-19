using MediatR;

namespace skit.Application.Technologies.Queries.BrowseTechnologies;

public sealed record BrowseTechnologiesQuery(string? Search = null) : IRequest<BrowseTechnologiesResponse>;
