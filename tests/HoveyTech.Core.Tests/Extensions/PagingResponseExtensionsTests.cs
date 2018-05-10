using HoveyTech.Core.Extensions;
using HoveyTech.Core.Paging;
using Moq;
using Xunit;

namespace HoveyTech.Core.Tests.Extensions
{
    public class PagingResponseExtensionsTests
    {
        [Fact]
        public void HasMorePages_does_return_true_when_PageCount_greater_Than_page()
        {
            var pagedListMock = new Mock<IPagedList>();
            pagedListMock.Setup(x => x.PageCount).Returns(2);
            pagedListMock.Setup(x => x.Page).Returns(1);
            Assert.True(pagedListMock.Object.HasMorePages());
        }

        [Fact]
        public void HasMorePages_does_return_false_when_pageCount_and_page_equal()
        {
            var pagedListMock = new Mock<IPagedList>();
            pagedListMock.Setup(x => x.PageCount).Returns(2);
            pagedListMock.Setup(x => x.Page).Returns(2);
            Assert.False(pagedListMock.Object.HasMorePages());
        }
    }
}
