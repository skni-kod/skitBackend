using skit.Application.Technologies.DTO;

namespace skit.Application.Technologies.Queries.BrowseTechnologies;

public sealed record BrowseTechnologiesResponse(List<TechnologiesDto> Technologies);
