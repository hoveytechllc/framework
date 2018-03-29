#if !NET20
using System;
using System.Collections.Generic;
using System.Linq;

namespace HoveyTech.Core.Paging
{
    public class PagedTypedList<TSource, TDestination> : List<TDestination>, IPagedList<TDestination>
    {
        public int TotalCount { get; protected set; }

        public int PageCount { get; protected set; }

        public int Page { get; protected set; }

        public int PageSize { get; protected set; }

        public PagedTypedList(IPagedList<TSource> source, Func<TSource, TDestination> func)
        {
            TotalCount = source.TotalCount;
            PageCount = source.PageCount;
            Page = source.Page;
            PageSize = source.PageSize;

            AddRange(source.Select(func));
        }
    }

    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public int TotalCount { get; }

        public int PageCount { get; }

        public int Page { get; }

        public int PageSize { get; }

        public PagedList(IPagedList pagingProperties, IList<T> list)
        {
            TotalCount = pagingProperties.TotalCount;
            PageCount = pagingProperties.PageCount;
            Page = pagingProperties.Page;
            PageSize = pagingProperties.PageSize;

            AddRange(list);
        }

        public PagedList(IQueryable<T> source, IPagingRequest request)
            : this(source, request.Page, request.PageSize) { }

        public PagedList(IQueryable<T> source, int page, int pageSize)
        {
            if (page < 1)
                page = 1;

            TotalCount = source.Count();
            PageCount = GetPageCount(pageSize, TotalCount);
            Page = page;
            PageSize = pageSize;

            AddRange(source.Skip((Page - 1) * PageSize).Take(PageSize).ToList());
        }

        private int GetPageCount(int pageSize, int totalCount)
        {
            if (pageSize == 0)
                return 0;

            var remainder = totalCount % pageSize;
            return (totalCount / pageSize) + (remainder == 0 ? 0 : 1);
        }
    }

    public class FilteredPagedList<T, TFilter> : PagedList<T>, IFilteredPagedList<T, TFilter>
        where TFilter : IPagingRequest
    {
        public FilteredPagedList(TFilter filter, IPagedList pagingProperties, IList<T> list)
            : base(pagingProperties, list)
        {
            Filter = filter;
        }

        public FilteredPagedList(TFilter filter, IQueryable<T> source)
            : base(source, filter)
        {
            Filter = filter;
        }

        public TFilter Filter { get; }
    }
}

#endif