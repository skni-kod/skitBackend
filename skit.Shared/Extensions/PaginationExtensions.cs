using skit.Shared.Models;

namespace skit.Shared.Extensions;

public static class PaginationExtensions
{
    public static Task<PaginatedList<TDestination>> ToPaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable, PaginationRequest req, CancellationToken cancellationToken)
    {
        return PaginatedList<TDestination>.CreateAsync(queryable, req.PageNumber, req.PageSize, req.OrderBy, req.IsDesc, cancellationToken);
    }
}