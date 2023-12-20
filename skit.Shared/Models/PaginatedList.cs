using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace skit.Shared.Models;

public sealed class PaginatedList<T>
{
    public List<T> Items { get; }
    public int PageIndex { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }


    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize, string orderBy, bool isDesc, CancellationToken cancellationToken)
    {
        var count = await source.CountAsync(cancellationToken);
        var items = await source.ToListAsync(cancellationToken);
        
        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            var type = typeof(T);
            var properties = type.GetProperties().ToList();
            var propertyInfo = properties.FirstOrDefault(x => string.Equals(x.Name, orderBy, StringComparison.CurrentCultureIgnoreCase));

            if (propertyInfo is not null)
            {
                var propertyName = propertyInfo.Name;
                var propertyType = propertyInfo.PropertyType;
                
                var parameter = Expression.Parameter(typeof(T));
                var property = Expression.Property(parameter, propertyName);
                var lambda = Expression.Lambda(property, parameter);
                
                var methodName = isDesc ? "OrderByDescending" : "OrderBy";

                // Create the OrderBy expression and compile it
                var orderByExpression = Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new[] { typeof(T), propertyType },
                    items.AsQueryable().Expression, // Convert list to IQueryable
                    lambda
                );

                // Create a new IQueryable with the OrderBy expression
                items = items.AsQueryable().Provider.CreateQuery<T>(orderByExpression).ToList();
            }
        }

        var pagedItems = items.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

        return new PaginatedList<T>(pagedItems, count, pageIndex, pageSize);
    }
}