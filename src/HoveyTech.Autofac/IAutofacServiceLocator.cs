using Autofac;
using Autofac.Core;
using HoveyTech.Core.Contracts.IoC;

namespace HoveyTech.Autofac
{
    public interface IAutofacServiceLocator : IServiceLocator
    {
        IContainer Container { get; }

        void Initialize(IContainer container);

        T Resolve<T>(params Parameter[] parameters);
    }
}
