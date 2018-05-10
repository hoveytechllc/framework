using HoveyTech.Core.Contracts.Data;
using HoveyTech.Core.Contracts.Model;

namespace HoveyTech.Core.Contracts.Caching
{
    public interface IEntityCacheService<TEntity> : IPagingRepository<TEntity>
        where TEntity : class, IGetIdentifier
    {
        void ClearCache();
    }
}
