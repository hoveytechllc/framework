using HoveyTech.Core.Model;
using Xunit;

namespace HoveyTech.Core.Tests.Model
{
    public class BaseEntityWithIntKeyTests
    {
        [Fact]
        public void IsNew_is_true_when_first_created()
        {
            var entity = new TestableBaseEntityWithIntKey();
            Assert.True(entity.IsNew);
            Assert.Equal(0, entity.Id);
        }

        [Fact]
        public void IsNew_is_false_when_id_not_zero()
        {
            var entity = new TestableBaseEntityWithIntKey();
            entity.SetId(1);
            Assert.False(entity.IsNew);
            Assert.Equal(1, entity.Id);
        }

        [Fact]
        public void GetIdentifier_does_return_id()
        {
            var entity = new TestableBaseEntityWithIntKey();
            entity.SetId(2);
            Assert.Equal(2, entity.GetIdentifier());
        }
    }

    public class TestableBaseEntityWithIntKey : BaseEntityWithIntKey
    {
        public void SetId(int id)
        {
            Id = id;
        }
    }
}
