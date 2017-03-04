using System;
using System.Collections.Generic;

namespace HoveyTech.Core.Contracts.IoC
{
    public interface IServiceLocator
    {
        bool IsInitialized { get; }

        T Resolve<T>();

        IEnumerable<T> ResolveAll<T>();

        T ResolveOptional<T>() where T : class;

        object Resolve(Type type);
    }
}
