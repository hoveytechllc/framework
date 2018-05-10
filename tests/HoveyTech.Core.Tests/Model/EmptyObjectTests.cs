using HoveyTech.Core.Runtime;
using Xunit;

namespace HoveyTech.Core.Tests.Model
{
    public class EmptyObjectTests
    {
        [Fact]
        public void Is_same_instance()
        {
            var empty1 = EmptyObject.Instance;
            var empty2 = EmptyObject.Instance;
            Assert.True(empty1 == empty2);
        }
    }
}
