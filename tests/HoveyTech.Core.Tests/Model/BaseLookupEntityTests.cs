using HoveyTech.Core.Model;
using Xunit;

namespace HoveyTech.Core.Tests.Model
{
    public class BaseLookupEntityTests
    {
        [Fact]
        public void Name_does_have_property()
        {
            var lookup = new TestableBaseLookupEntity("item 1", true);
            Assert.Equal("item 1", lookup.Name);
            Assert.True(lookup.IsActive);
        }

        [Fact]
        public void IsActive_is_true_by_default()
        {
            var lookup = new TestableBaseLookupEntity();
            Assert.True(lookup.IsActive);
        }
    }

    public class TestableBaseLookupEntity : BaseLookupEntity
    {
        public TestableBaseLookupEntity(string name, bool isActive = true, int id = 0)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
        }

        public TestableBaseLookupEntity()
        {
            
        }
    }
}
