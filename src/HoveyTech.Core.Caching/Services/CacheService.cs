using System;
using HoveyTech.Core.Contracts.Caching;

namespace HoveyTech.Core.Caching.Services
{
    public class CacheService<T> : ICacheService<T>
        where T : class
    {
        protected readonly ICacheService CacheServiceInternal;
        protected readonly Type Key;

        public CacheService(ICacheService cacheServiceInternal)
        {
            CacheServiceInternal = cacheServiceInternal;
            Key = typeof(T);
        }

        public virtual T Get(Func<T> initializeCacheOnEmptyFunc = null)
        {
            lock (this)
            {
                return (T)CacheServiceInternal.Get(Key, initializeCacheOnEmptyFunc);
            }
        }

        public virtual void Set(T value)
        {
            lock (this)
            {
                CacheServiceInternal.Set(Key, value);
            }
        }

        public virtual void Clear()
        {
            lock (this)
            {
                CacheServiceInternal.Clear(Key);
            }
        }
    }
}
