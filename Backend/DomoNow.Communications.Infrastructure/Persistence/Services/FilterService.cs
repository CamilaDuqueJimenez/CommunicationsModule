using DomoNow.Communications.Application.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DomoNow.Communications.Infrastructure.Persistence.Services
{
    public static class FilterService
    {
        public static IQueryable<T> ApplyFiltering<T>(this IQueryable<T> query, QueryParam filter, params Expression<Func<T, string?>>[] searchProps)
        {
            if (!string.IsNullOrWhiteSpace(filter.Search) && searchProps?.Length > 0)
            {
                var pattern = BuildLikePattern(filter.Search);

                Expression? orBody = null;
                var parameter = Expression.Parameter(typeof(T), "e");

                var likeMethod = typeof(DbFunctionsExtensions).GetMethod(
                    nameof(DbFunctionsExtensions.Like),
                    new[] { typeof(DbFunctions), typeof(string), typeof(string) });

                foreach (var prop in searchProps)
                {
                    var invoked = Expression.Invoke(prop, parameter);

                    var coalesced = Expression.Coalesce(invoked, Expression.Constant(string.Empty));

                    var efFunctions = Expression.Property(null, typeof(EF), nameof(EF.Functions));

                    var likeCall = Expression.Call(likeMethod!, efFunctions, coalesced, Expression.Constant(pattern));
                    orBody = orBody == null ? likeCall : Expression.OrElse(orBody, likeCall);
                }

                if (orBody != null)
                {
                    var lambda = Expression.Lambda<Func<T, bool>>(orBody, parameter);
                    query = query.Where(lambda);
                }
            }

            if (!string.IsNullOrWhiteSpace(filter.OrderBy))
            {
                query = filter.IsAscending
                    ? query.OrderBy(x => EF.Property<object>(x, filter.OrderBy))
                    : query.OrderByDescending(x => EF.Property<object>(x, filter.OrderBy));
            }
            else
            {
                var createdAtProp = typeof(T).GetProperty("CreatedAt");
                if (createdAtProp != null)
                {
                    if (createdAtProp.PropertyType == typeof(DateTime?))
                        query = query.OrderByDescending(x => EF.Property<DateTime?>(x, "CreatedAt"));
                    else
                        query = query.OrderByDescending(x => EF.Property<DateTime>(x, "CreatedAt"));
                }
                else if (typeof(T).GetProperty("Name") != null)
                {
                    query = query.OrderBy(x => EF.Property<string>(x, "Name"));
                }
            }

            if (filter.StartDate.HasValue)
            {
                var start = filter.StartDate.Value.Date;
                query = query.Where(x => EF.Property<DateTime>(x, "CreatedAt") >= start);
            }
            if (filter.EndDate.HasValue)
            {
                var endExclusive = filter.EndDate.Value.Date.AddDays(1);
                query = query.Where(x => EF.Property<DateTime>(x, "CreatedAt") < endExclusive);
            }
            if (filter.CreatedAt.HasValue)
            {
                var day = filter.CreatedAt.Value.Date;
                var next = day.AddDays(1);
                query = query.Where(x => EF.Property<DateTime>(x, "CreatedAt") >= day && EF.Property<DateTime>(x, "CreatedAt") < next);
            }

            return query;
        }

        private static string BuildLikePattern(string search)
        {
            return $"%{search.Trim()}%";
        }
    }
}
