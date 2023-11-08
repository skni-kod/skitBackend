using skit.Shared.Abstractions.Queries;

namespace skit.Shared.Abstractions;

public static class Extensions
{
    public static Task<Paged<TDestination>> PaginateAsync<TDestination>(
        this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
    {
        return Paged<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
    }

    public static Task<Paged<TDestination>> PaginateAsync<TDestination>(
        this IQueryable<TDestination> queryable, PaginationRequest req)
    {
        return Paged<TDestination>.CreateAsync(queryable, req.PageNumber, req.PageSize);
    }
}