using Autofac;
using Autofac.Core;
using HoveyTech.Core.Contracts.Containers;

namespace HoveyTech.Core.Autofac
{
    public interface IAutofacServiceContainer : IServiceContainer
    {
        IContainer Container { get; }
        
        T Resolve<T>(params Parameter[] parameters);
    }
}
