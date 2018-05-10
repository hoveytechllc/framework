using HoveyTech.Core.Paging;

namespace HoveyTech.Core.Extensions
{
    public static class PagingResponseExtensions
    {
        public static bool HasMorePages(this IPagedList list)
        {
            return list.PageCount > list.Page;
        }
    }
}
