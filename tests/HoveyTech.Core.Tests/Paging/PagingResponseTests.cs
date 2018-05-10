using System.Collections.Generic;
using System.Linq;
using HoveyTech.Core.Paging;
using Xunit;

namespace HoveyTech.Core.Tests.Paging
{
    public class PagingResponseTests
    {
        [Fact]
        public void ctor_does_set_properties_from_pagedList()
        {
            var items = new List<string>();
            for (var i = 0; i < 100; i++) { items.Add($"Item {i}"); }

            var pagedList = new PagedList<string>(items.AsQueryable(), 2, 22);
            var response = new PagingResponse<string>(pagedList);

            Assert.Equal(pagedList.Page, response.Page);
            Assert.Equal(pagedList.PageSize, response.PageSize);
            Assert.Equal(pagedList.TotalCount, response.TotalCount);
            Assert.Equal(pagedList.PageCount, response.PageCount);
            Assert.Equal(pagedList.Count, response.Result.Count);
        }
    }
}
