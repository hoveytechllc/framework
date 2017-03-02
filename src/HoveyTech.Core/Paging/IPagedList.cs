using System.Collections.Generic;

namespace HoveyTech.Core.Paging
{
    public interface IPagedList<T> : IList<T>, IPagedList
    {

    }

    public interface IPagedList
    {
        int TotalCount { get; }

        int PageCount { get; }

        int Page { get; }

        int PageSize { get; }
    }

    public interface IFilteredPagedList<out TFilter> : IPagedList
        where TFilter : IPagingRequest
    {
        TFilter Filter { get; }
    }

    public interface IFilteredPagedList<T, out TFilter> : IPagedList<T>, IFilteredPagedList<TFilter>
        where TFilter : IPagingRequest
    {
    }
}
