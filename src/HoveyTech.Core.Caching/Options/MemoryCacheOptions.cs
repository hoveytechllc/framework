using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace HoveyTech.Core.Caching.Options
{
    public class DefaultMemoryCacheOptions : IOptions<MemoryCacheOptions>
    {
        public MemoryCacheOptions Value => new MemoryCacheOptions();
    }
}
