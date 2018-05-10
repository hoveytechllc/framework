using System;
using System.IO;
using System.Threading.Tasks;
using HoveyTech.Core.Caching.Options;
using HoveyTech.Core.Contracts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;

namespace HoveyTech.Core.Caching.Services
{
    public class FileTriggeredCacheService<T> : CacheService<T>, IFileTriggeredCacheService<T>
        where T : class
    {
        private readonly CacheService _cacheService;
        private readonly IFileTriggeredCacheConfiguration _fileDependentCacheConfiguration;
        private readonly IDateTimeFactory _dateTimeFactory;

        public FileTriggeredCacheService(CacheService cacheService,
            IFileTriggeredCacheConfiguration fileDependentCacheConfiguration,
            IDateTimeFactory dateTimeFactory)
            : base(cacheService)
        {
            _cacheService = cacheService;
            _fileDependentCacheConfiguration = fileDependentCacheConfiguration;
            _dateTimeFactory = dateTimeFactory;
        }

        public override T Get(Func<T> initializeCacheOnEmptyFunc = null)
        {
            T FuncOverride()
            {
                var result = initializeCacheOnEmptyFunc?.Invoke();

                try
                {
                    // This ensures that when cache is populated
                    // a trigger file is created. This makes it easier
                    // for an administrator to reset the cache for 
                    // application if the trigger file already exists.
                    if (initializeCacheOnEmptyFunc != null
                        && !File.Exists(GetTriggerFilePath()))
                        ChangeContentsOfWatchedFile();
                }
                catch
                {
                }

                return result;
            }

            lock (this)
            {
                return (T)CacheServiceInternal.Get(Key, FuncOverride);
            }
        }

        public override void Set(T value)
        {
            lock (this)
            {
                _cacheService.MemoryCache.Set(Key, value, GetEntryOptions());
                ChangeContentsOfWatchedFile();
            }
        }

        public override void Clear()
        {
            lock (this)
            {
                _cacheService.MemoryCache.Remove(Key);
                ChangeContentsOfWatchedFile();
            }
        }

        private string GetTriggerFilePath()
        {
            return Path.Combine(GetCachePathAndEnsureCreated(),
                 GetFilenameOfWatchedFile());
        }

        private void ChangeContentsOfWatchedFile(int tries = 1)
        {
            var filePath = GetTriggerFilePath();

            try
            {
                File.WriteAllText(filePath, _dateTimeFactory.UtcNowWithOffset.ToString("O"));
            }
            catch
            {
                if (tries > 3)
                    throw;

                Task.Delay(TimeSpan.FromMilliseconds(50)).Wait();
                ChangeContentsOfWatchedFile(++tries);
            }
        }

        private MemoryCacheEntryOptions GetEntryOptions()
        {
            var fileProvider = new PhysicalFileProvider(GetCachePathAndEnsureCreated());

            var options = new MemoryCacheEntryOptions()
                .AddExpirationToken(fileProvider.Watch(GetFilenameOfWatchedFile()));

            return options;
        }

        public string GetFilenameOfWatchedFile()
        {
            return $"CacheEntry_{Key.FullName}.txt";
        }

        private string GetCachePathAndEnsureCreated()
        {
            var path = _fileDependentCacheConfiguration.GetWatchDirectory();

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
    }
}
