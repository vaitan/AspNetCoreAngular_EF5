using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AspNetCoreAngular_EF5.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject filter, Dictionary<string, Expression<Func<T, object>>> sortQuery)
        {
            if (filter.SortBy == null || filter.SortBy == string.Empty) return query;

            if (filter.IsAscending)
                return query.OrderBy(sortQuery[filter.SortBy]);
            else
                return query.OrderByDescending(sortQuery[filter.SortBy]);
        }
    }
}