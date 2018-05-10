using HoveyTech.Core.Paging;
using Xunit;

namespace HoveyTech.Core.Tests.Paging
{
    public class PagingRequestTests
    {
        [Fact]
        public void PreviousPage_does_not_decrement_page_if_1()
        {
            var request = new PagingRequest();
            request.PreviousPage();
            Assert.Equal(1, request.Page);
        }

        [Fact]
        public void NextPage_does_not_increment_page()
        {
            var request = new PagingRequest();
            request.NextPage();
            Assert.Equal(2, request.Page);
        }

        [Fact]
        public void PageSize_is_20_by_default()
        {
            var request = new PagingRequest();
            Assert.Equal(20, request.PageSize);
        }
    }
}
