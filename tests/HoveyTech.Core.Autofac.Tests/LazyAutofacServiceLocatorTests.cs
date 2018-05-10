using Autofac;
using HoveyTech.Core.Contracts;
using HoveyTech.Core.Services;
using Xunit;

namespace HoveyTech.Core.Autofac.Tests
{
    public class TestableLazyAutofacServiceLocator : LazyAutofacServiceContainer
    {
        public int BuildCount { get; private set; }

        protected override void SetupContainerBuilder(ContainerBuilder builder)
        {
            BuildCount++;
            builder.RegisterType<DateTimeFactory>().AsImplementedInterfaces();
        }
    }

    public class LazyAutofacServiceLocatorTests
    {
        [Fact]
        public void Resolve_does_build_once_if_called_multiple_times()
        {
            var sut = new TestableLazyAutofacServiceLocator();

            sut.Resolve<IDateTimeFactory>();
            sut.Resolve<IDateTimeFactory>();
            sut.Resolve<IDateTimeFactory>();

            Assert.Equal(1, sut.BuildCount);
        }
        
        [Fact]
        public void Container_does_build_container()
        {
            var sut = new TestableLazyAutofacServiceLocator();

            var container1 = sut.Container;
            var container2 = sut.Container;
            var container3 = sut.Container;
            
            Assert.Equal(1, sut.BuildCount);
            Assert.Equal(container1, container2);
            Assert.Equal(container2, container3);
        }
    }
}
