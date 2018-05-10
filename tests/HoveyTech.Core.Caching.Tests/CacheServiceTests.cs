using HoveyTech.Core.Caching.Services;
using HoveyTech.Core.Contracts.Caching;
using Xunit;

namespace HoveyTech.Core.Caching.Tests
{
    public class CacheServiceTests
    {
        [Fact]
        public void Get_does_return_null_if_nothing_set()
        {
            var context = new TestableCacheService();
            Assert.Null(context.SutInMemoryObject.Get());
        }

        [Fact]
        public void Get_does_call_func_if_provided_and_cache_empty()
        {
            var context = new TestableCacheService();
            var called = false;
            var cache = context.SutInMemoryObject.Get(() =>
            {
                called = true;
                return new InMemoryObject("is.called");
            });
            Assert.True(called);
            Assert.NotNull(cache);
            Assert.Equal("is.called", cache.Value);
        }

        [Fact]
        public void Get_does_not_call_func_if_cache_present()
        {
            var context = new TestableCacheService();
            context.SutInMemoryObject.Set(new InMemoryObject("stays"));
            var called = false;
            var cache = context.SutInMemoryObject.Get(() =>
            {
                called = true;
                return new InMemoryObject("never.reached");
            });
            Assert.False(called);
            Assert.NotNull(cache);
            Assert.Equal("stays", cache.Value);
        }

        [Fact]
        public void Remove_does_clear_cache()
        {
            var context = new TestableCacheService();
            context.SutInMemoryObject.Set(new InMemoryObject("stays"));
            context.SutInMemoryObject.Clear();
            Assert.Null(context.SutInMemoryObject.Get());
        }

        [Fact]
        public void ClearAll_does_clear_everything()
        {
            var context = new TestableCacheService();
            context.Sut.Set(typeof(InMemoryObject), new InMemoryObject("stays"));
            context.Sut.Set(typeof(string), "hello");

            var internalCache = context.Sut.MemoryCache;

            context.Sut.ClearAll();

            context.Sut.MemoryCache.TryGetValue(typeof(string), out object stringValue);
            context.Sut.MemoryCache.TryGetValue(typeof(InMemoryObject), out object testObject);
            
            Assert.Null(stringValue);
            Assert.Null(testObject);
            Assert.NotEqual(internalCache, context.Sut.MemoryCache);
        }
    }

    public class InMemoryObject
    {
        public string Value { get; }

        public InMemoryObject(string value)
        {
            Value = value;
        }
    }

    public class TestableCacheService
    {
        public CacheService Sut { get; }
        public ICacheService<InMemoryObject> SutInMemoryObject { get; }

        public TestableCacheService()
        {
            Sut = new CacheService();
            SutInMemoryObject = new CacheService<InMemoryObject>(Sut);
        }
    }
}
