using System.Linq;
using HoveyTech.Core.Extensions;
using HoveyTech.Core.Paging;
using HoveyTech.Core.Tests.Paging;
using Xunit;

namespace HoveyTech.Core.Tests.Extensions
{
    public class PagedListExtensions
    {
        [Fact]
        public void ToResponse_does_construct_PagingResponse()
        {
            var pagedList = TestablePagedList.Create();
            var response = pagedList.ToResponse();
            Assert.True(response.Success);
            Assert.Equal(response.Page, pagedList.Page);
            Assert.Equal(response.PageSize, pagedList.PageSize);
            Assert.Equal(response.TotalCount, pagedList.TotalCount);
            Assert.Equal(response.PageCount, pagedList.PageCount);
        }

        [Fact]
        public void ToResponse_does_construct_PagingResponse_with_filter()
        {
            IFilteredPagedList<TestEntity, PagingRequest> pagedList = new FilteredPagedList<TestEntity, PagingRequest>(new PagingRequest(),
                TestablePagedList.CreateEntities().AsQueryable());
            var response = pagedList.ToResponse();
            Assert.True(response.Success);
            Assert.Equal(response.Page, pagedList.Page);
            Assert.Equal(response.PageSize, pagedList.PageSize);
            Assert.Equal(response.TotalCount, pagedList.TotalCount);
            Assert.Equal(response.PageCount, pagedList.PageCount);
        }

        [Fact]
        public void GetDescription_does_return_correct_description()
        {
            var pagedList = TestablePagedList.Create(
                TestablePagedList.CreateEntities(40),
                new PagingRequest()
                {
                    Page = 2,
                    PageSize = 10
                });

            var description = pagedList.GetDescription();
            Assert.Equal("Showing 10 of 40 results (page 2 of 4)...", description);
        }
    }
}
