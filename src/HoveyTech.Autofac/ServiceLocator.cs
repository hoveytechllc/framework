using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;

namespace HoveyTech.Autofac
{
    public class ServiceLocator : IAutofacServiceLocator
    {
        private static ServiceLocator _instance;
        private static readonly object InstanceLock = new object();

        public static ServiceLocator Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    return _instance ?? (_instance = new ServiceLocator());
                }
            }  
        } 

        private ServiceLocator() { }

        public bool IsInitialized { get; private set; }

        public IContainer Container { get; private set; }

        private void ThrowIfNotInitialized()
        {
            if (!IsInitialized)
                throw new Exception("ServiceLocator has not been initialized.");
        }

        public void Initialize(IContainer container)
        {
            Container = container;
            IsInitialized = true;
        }

        public T Resolve<T>()
        {
            ThrowIfNotInitialized();
            return Container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            ThrowIfNotInitialized();
            return Container.Resolve(type);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            ThrowIfNotInitialized();
            return Container.Resolve<IEnumerable<T>>();
        }

        public T Resolve<T>(params Parameter[] parameters)
        {
            ThrowIfNotInitialized();
            return Container.Resolve<T>(parameters);
        }

        public T ResolveOptional<T>() where T : class
        {
            ThrowIfNotInitialized();
            return Container.ResolveOptional<T>();
        }
    }
}
