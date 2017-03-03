using Autofac;
using HoveyTech.Core.Contracts.Data;

namespace HoveyTech.Data.EfCore.Autofac
{
    public class HoveyTechDataEfCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<,>));
        }
    }
}
