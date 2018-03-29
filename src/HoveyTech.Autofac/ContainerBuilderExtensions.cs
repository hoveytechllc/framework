using Autofac;
using HoveyTech.Core;
using HoveyTech.Core.Contracts.IoC;
using HoveyTech.Core.Services;

namespace HoveyTech.Autofac
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterDateTimeFactory(this ContainerBuilder builder)
        {
            builder.RegisterType<DateTimeFactory>().AsImplementedInterfaces().AsSelf();
        }

        public static void InitializeServiceLocator(this ContainerBuilder builder,
            IAutofacServiceLocator serviceLocator)
        {
            builder.RegisterInstance(serviceLocator).
                As<IServiceLocator>().
                As<IAutofacServiceLocator>();

            serviceLocator.Initialize(builder.Build());
        }
    }
}
