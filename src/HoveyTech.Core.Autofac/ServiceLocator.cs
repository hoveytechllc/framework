using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;

namespace HoveyTech.Core.Autofac
{
    public class AutofacServiceContainer : IAutofacServiceContainer
    {
        public bool IsInitialized => Container != null;
        public IContainer Container { get; protected set; }

        public AutofacServiceContainer()
        {

        }

        public AutofacServiceContainer(ContainerBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            Build(builder);
        }

        public AutofacServiceContainer(IContainer container)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
        }

        private void ThrowIfNotInitialized()
        {
            if (!IsInitialized)
                throw new Exception("AutofacServiceLocator has not been initialized.");
        }

        private void Build(ContainerBuilder builder)
        {
            Container = builder.Build();
        }
        
        public virtual void Initialize(IContainer container)
        {
            Container = container;
        }

        public virtual void Initialize(ContainerBuilder builder)
        {
            Build(builder);
        }

        public virtual T Resolve<T>()
        {
            ThrowIfNotInitialized();
            return Container.Resolve<T>();
        }

        public virtual object Resolve(Type type)
        {
            ThrowIfNotInitialized();
            return Container.Resolve(type);
        }

        public virtual IEnumerable<T> ResolveAll<T>()
        {
            ThrowIfNotInitialized();
            return Container.Resolve<IEnumerable<T>>();
        }

        public virtual T Resolve<T>(params Parameter[] parameters)
        {
            ThrowIfNotInitialized();
            return Container.Resolve<T>(parameters);
        }

        public virtual T ResolveOptional<T>() where T : class
        {
            ThrowIfNotInitialized();
            return Container.ResolveOptional<T>();
        }
    }
}
