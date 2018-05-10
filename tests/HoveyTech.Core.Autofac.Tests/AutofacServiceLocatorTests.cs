using Autofac;
using Xunit;

namespace HoveyTech.Core.Autofac.Tests
{
    public class AutofacServiceLocatorTests
    {
        public class TestRegisteredObject
        {

        }

        [Fact]
        public void Resolve_does_return_instance_of_registration()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestRegisteredObject>();

            var sut = new AutofacServiceContainer(builder.Build());
            
            TestRegisteredObject result = sut.Resolve<TestRegisteredObject>();
            Assert.NotNull(result);
        }
    }
}
