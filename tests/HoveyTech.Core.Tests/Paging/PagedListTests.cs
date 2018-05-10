using System.Collections.Generic;
using System.Linq;
using HoveyTech.Core.Extensions;
using HoveyTech.Core.Paging;
using Xunit;

namespace HoveyTech.Core.Tests.Paging
{
    public class TestEntity
    {
        public int Number { get; set; }
    }

    public class PagedListTests
    {
        [Fact]
        public void ctor_does_calculate_page_Count()
        {
            var sut = TestablePagedList.Create();
            
            Assert.Equal(2, sut.PageCount);
            Assert.Equal(1, sut.Page);
            Assert.Equal(20, sut.PageSize);
            Assert.Equal(40, sut.TotalCount);
        }

        [Fact]
        public void ctor_does_return_second_page()
        {
            var request = new PagingRequest();
            request.NextPage();

            var sut = TestablePagedList.Create(request: request);

            for (var i = 0; i < sut.Count; i++)
            {
                var number = i + 21;
                Assert.Equal(number, sut[i].Number);
            }
        }
    }

    public class TestablePagedList : PagedList<TestEntity>
    {
        public TestablePagedList(IPagedList pagingProperties, IList<TestEntity> list) : base(pagingProperties, list)
        {
        }

        public TestablePagedList(IQueryable<TestEntity> source, IPagingRequest request) : base(source, request)
        {
        }

        public TestablePagedList(IQueryable<TestEntity> source, int page, int pageSize) : base(source, page, pageSize)
        {
        }

        public static IList<TestEntity> CreateEntities(int count = 40)
        {
            var items = new List<TestEntity>();

            for (var i = 0; i < count; i++)
            {
                var item = new TestEntity();
                item.Number = i + 1;
                items.Add(item);
            }

            return items;
        }

        public static IPagedList<TestEntity> Create(IList<TestEntity> entities = null, IPagingRequest request = null)
        {
            entities = entities ?? CreateEntities();
            request = request ?? new PagingRequest();

            return entities.AsQueryable().ToPagedList(request);
        }
    }
}
