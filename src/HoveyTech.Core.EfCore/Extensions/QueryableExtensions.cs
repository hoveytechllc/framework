using System;
using System.Linq;
using System.Linq.Expressions;
using HoveyTech.Core.Paging;

namespace HoveyTech.Core.EfCore.Extensions
{
    public static class QueryableExtensions
    {
        public static FilteredPagedList<T, TFilter> ToFilteredPagedList<T, TFilter>(this IQueryable<T> source, TFilter request)
            where TFilter : IPagingRequest
        {
            return new FilteredPagedList<T, TFilter>(request, source);
        }

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, IPagingRequest request)
        {
            return new PagedList<T>(source, request);
        }

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int page, int pageSize)
        {
            return new PagedList<T>(source, page, pageSize);
        }

        public static IOrderedQueryable<T> Order<T>(this IQueryable<T> source, string propertyName, bool ascending, bool anotherLevel = false)
        {
            var param = Expression.Parameter(typeof(T), String.Empty);
            var property = Expression.PropertyOrField(param, propertyName);
            var sort = Expression.Lambda(property, param);

            var call = Expression.Call(
                typeof(Queryable),
                (!anotherLevel ? "OrderBy" : "ThenBy") +
                (ascending ? String.Empty : "Descending"),
                new[] { typeof(T), property.Type },
                source.Expression,
                Expression.Quote(sort));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }
    }
}
