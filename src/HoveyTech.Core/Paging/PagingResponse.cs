using System.Collections.Generic;

namespace HoveyTech.Core.Paging
{
    public class PagingResponse<T> : Response<IList<T>>, IPagedList
    {
        public int TotalCount { get; set; }

        public int PageCount { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public PagingResponse()
        {
            
        }

        public PagingResponse(IPagedList<T> pagedList)
            :base(pagedList)
        {
            TotalCount = pagedList.TotalCount;
            Page = pagedList.Page;
            PageCount = pagedList.PageCount;
            PageSize = pagedList.PageSize;
        }
    }
}
