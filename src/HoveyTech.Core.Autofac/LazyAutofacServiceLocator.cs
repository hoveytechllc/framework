using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Core;

namespace HoveyTech.Core.Autofac
{
    public abstract class LazyAutofacServiceContainer : IAutofacServiceContainer
    {
        public virtual bool IsInitialized => _container != null;

        private IContainer _container;
        public virtual IContainer Container
        {
            get
            {
                if (_container != null)
                    return _container;

                var builder = new ContainerBuilder();
                SetupContainerBuilder(builder);
                builder.RegisterInstance(this).AsSelf().AsImplementedInterfaces();
                
                _container = builder.Build();
                return _container;
            }
        }

        protected abstract void SetupContainerBuilder(ContainerBuilder builder);
        
        public virtual T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public virtual object Resolve(Type type)
        {
            return Container.Resolve(type);
        }

        public virtual IEnumerable<T> ResolveAll<T>()
        {
            return Container.Resolve<IEnumerable<T>>();
        }

        public virtual T Resolve<T>(params Parameter[] parameters)
        {
            return Container.Resolve<T>(parameters);
        }

        public virtual T ResolveOptional<T>() where T : class
        {
            return Container.ResolveOptional<T>();
        }
    }
}
