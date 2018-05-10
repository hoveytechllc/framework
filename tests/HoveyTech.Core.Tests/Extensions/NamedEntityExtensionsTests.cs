using System.Collections.Generic;
using System.Linq;
using HoveyTech.Core.Extensions;
using HoveyTech.Core.Tests.Model;
using Xunit;

namespace HoveyTech.Core.Tests.Extensions
{
    public class NamedEntityExtensionsTests
    {
        [Fact]
        public void FindNameById_does_return_name_of_first_entry()
        {
            var items = Create();
            var item = items.FindNameOfId(0);
            Assert.Equal("Item 0", item);
        }
        
        [Fact]
        public void FindNameById_does_return_unknown_if_cannot_find_by_id()
        {
            var items = Create();
            var item = items.FindNameOfId(101);
            Assert.Equal("Unknown", item);
        }

        [Fact]
        public void FindByFilterText_does_return_full_list_if_filter_null()
        {
            var items = new List<TestableBaseLookupEntity>()
            {
                new TestableBaseLookupEntity("Item 1"),
                new TestableBaseLookupEntity("Item 2")
            };
            var results = items.FindByFilterText(null).ToList();
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public void FindByFilterText_does_return_full_list_if_filter_empty()
        {
            var items = new List<TestableBaseLookupEntity>()
            {
                new TestableBaseLookupEntity("Item 1"),
                new TestableBaseLookupEntity("Item 2")
            };
            var results = items.FindByFilterText("").ToList();
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public void FindByFilterText_does_sort_by_name()
        {
            var items = new List<TestableBaseLookupEntity>()
            {
                new TestableBaseLookupEntity("Item 2"),
                new TestableBaseLookupEntity("Item 1")
            };
            var results = items.FindByFilterText("").ToList();
            Assert.Equal("Item 1", results.ElementAt(0).Name);
            Assert.Equal("Item 2", results.ElementAt(1).Name);
        }

        [Fact]
        public void FindByFilterText_does_not_include_InActive()
        {
            var items = new List<TestableBaseLookupEntity>()
            {
                new TestableBaseLookupEntity("Item 1"),
                new TestableBaseLookupEntity("Item 2", isActive:false),
            };
            var results = items.FindByFilterText(null).ToList();
            Assert.Single(results);
            Assert.Equal("Item 1", results.ElementAt(0).Name);
        }

        [Fact]
        public void FindByFilterText_does_return_items_filtered()
        {
            var items = new List<TestableBaseLookupEntity>()
            {
                new TestableBaseLookupEntity("Item 1"),
                new TestableBaseLookupEntity("Item 2", isActive:false),
                new TestableBaseLookupEntity("Another one")
            };
            var results = items.FindByFilterText("Item ").ToList();
            Assert.Single(results);
            Assert.Equal("Item 1", results.ElementAt(0).Name);
        }

        public IList<TestableBaseLookupEntity> Create()
        {
            var items = new List<TestableBaseLookupEntity>();
            for (var i = 0; i < 100; i++) {  items.Add(new TestableBaseLookupEntity($"Item {i}", true, i));}
            return items;
        }
    }
}
