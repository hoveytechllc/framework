using System;
using HoveyTech.Core.Caching.Options;
using HoveyTech.Core.Contracts.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace HoveyTech.Core.Caching.Services
{
    public class CacheService : ICacheService
    {
        private IMemoryCache _memoryCache;
        internal IMemoryCache MemoryCache
        {
            get
            {
                lock (this)
                {
                    return _memoryCache = _memoryCache ?? new MemoryCache(new DefaultMemoryCacheOptions());
                }
            }
        }
        
        public virtual object Get(Type key, Func<object> initializeCacheOnEmptyFunc = null)
        {
            lock (this)
            {
                var value = MemoryCache.Get(key);

                if (value != null)
                    return value;

                if (initializeCacheOnEmptyFunc == null)
                    return null;

                value = initializeCacheOnEmptyFunc();
                Set(key, value);
                return value;
            }
        }

        public virtual void Set(Type key, object value)
        {
            lock (this)
            {
                MemoryCache.Set(key, value);
            }
        }

        public virtual void Clear(Type key)
        {
            lock (this)
            {
                MemoryCache.Remove(key);
            }
        }

        public virtual void ClearAll()
        {
            lock (this)
            {
                _memoryCache?.Dispose();
                _memoryCache = null;
            }
        }
    }

}
