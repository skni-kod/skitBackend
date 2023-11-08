using skit.Shared.Abstractions.Models;

namespace skit.Shared.Abstractions.Extensions;

public static class PaginationExtensions
{
    public static Task<PaginatedList<TDestination>> ToPaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable, PaginationRequest req)
    {
        return PaginatedList<TDestination>.CreateAsync(queryable, req.PageNumber, req.PageSize);
    }
}