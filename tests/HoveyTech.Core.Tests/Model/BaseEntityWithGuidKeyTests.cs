using System;
using HoveyTech.Core.Model;
using Xunit;

namespace HoveyTech.Core.Tests.Model
{
    public class BaseEntityWithGuidKeyTests
    {
        [Fact]
        public void IsNew_is_true_when_first_created()
        {
            var entity = new TestableBaseEntityWithGuidKey();
            Assert.True(entity.IsNew);
            Assert.Equal(Guid.Empty, entity.Id);
        }

        [Fact]
        public void IsNew_is_false_when_id_not_zero()
        {
            var id = Guid.NewGuid();
            var entity = new TestableBaseEntityWithGuidKey();
            entity.SetId(id);
            Assert.False(entity.IsNew);
            Assert.Equal(id, entity.Id);
        }

        [Fact]
        public void GetIdentifier_does_return_id()
        {
            var id = Guid.NewGuid();
            var entity = new TestableBaseEntityWithGuidKey();
            entity.SetId(id);
            Assert.Equal(id, entity.GetIdentifier());
        }
    }

    public class TestableBaseEntityWithGuidKey : BaseEntityWithGuidKey
    {
        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
