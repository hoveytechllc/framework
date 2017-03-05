using System.Reflection;
using Autofac;
using HoveyTech.Core.Contracts.Data;
using HoveyTech.Core.Extensions;

namespace HoveyTech.Data.EfCore.Autofac
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterRepositoriesFrom(this ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly).
                Where(x => 
                    x.IsDerivedFromGeneric(typeof(IRepository<>))).
                AsImplementedInterfaces().
                AsSelf();
        }
    }
}
