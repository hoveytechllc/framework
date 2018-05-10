using System;

namespace HoveyTech.Core.Contracts.Caching
{
    public interface ICacheService 
    {
        object Get(Type key, Func<object> initializeCacheOnEmptyFunc = null);

        void Set(Type key, object value);

        void Clear(Type key);

        void ClearAll();
    }

    public interface ICacheService<T>
    {
        T Get(Func<T> initializeCacheOnEmptyFunc = null);

        void Set(T value);

        void Clear();
    }
}
