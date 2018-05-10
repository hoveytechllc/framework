using HoveyTech.Core.Paging;

namespace HoveyTech.Core.Extensions
{
    public static class PagedListExtensions
    {
        public static PagingResponse<T> ToResponse<T>(this IPagedList<T> pagedList)
        {
            return new PagingResponse<T>(pagedList);
        }

        public static PagingResponse<T> ToResponse<T, TFilter>(this IFilteredPagedList<T, TFilter> pagedList)
            where TFilter : IPagingRequest
        {
            return new PagingResponse<T>(pagedList);
        }

        public static string GetDescription<T>(this IPagedList<T> pagedList)
        {
            return GetDescription(pagedList, pagedList.Count);
        }

        public static string GetDescription(this IPagedList pagedList, int itemCount)
        {
            return $"Showing {itemCount} of {pagedList.TotalCount} results (page {pagedList.Page} of {pagedList.PageCount})...";
        }
    }
}
