using HoveyTech.Core.Extensions;
using Xunit;

namespace HoveyTech.Core.Tests.Extensions
{
    public class ObjectExtensionsTests
    {
        public class TestObject
        {
            
        }

        [Fact]
        public void ToResponse_does_return_success_response()
        {
            var obj = new TestObject();
            var response = obj.ToResponse();
            Assert.True(response.Success);
            Assert.True(obj == response.Result);
        }
    }
}
