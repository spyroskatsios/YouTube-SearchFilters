using System.Diagnostics;
using Customers.Entities;
using Customers.Shared;
using Microsoft.EntityFrameworkCore;

namespace Customers.Filters;

public static class BaseFilters
{
    public static IQueryable<T> ApplyBaseFilters<T>(this IQueryable<T> data, SearchFilters filters)
        where T : DomainEntity
    {
        if (!filters.SortBy.IsNullOrEmpty())
        {
            data = filters.SortOrder switch
            {
                EnSortOrder.Ascending => data.OrderBy(x => EF.Property<object>(x!, filters.SortBy!)).ThenBy(x => x.Id),
                EnSortOrder.Descending => data.OrderByDescending(x => EF.Property<object>(x!, filters.SortBy!))
                    .ThenBy(x => x.Id),
                _ => throw new UnreachableException()
            };
        }
        else
        {
            data = filters.SortOrder switch
            {
                EnSortOrder.Ascending => data.OrderBy(x=>x.Id),
                EnSortOrder.Descending => data.OrderByDescending(x =>x.Id),
                _ => throw new UnreachableException()
            };
        }

        return data.Skip((filters.PageNumber - 1) * filters.PageSize).Take(filters.PageSize);
    }
}