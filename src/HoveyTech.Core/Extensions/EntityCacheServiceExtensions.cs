using System.Collections.Generic;
using HoveyTech.Core.Contracts.Caching;
using HoveyTech.Core.Contracts.Model;

namespace HoveyTech.Core.Extensions
{
    public static class EntityCacheServiceExtensions
    { 
        public static IList<TEntity> GetAllActiveSorted<TEntity>(this IEntityCacheService<TEntity> items, int? id = null)
            where TEntity : class, IIsActive, INamedEntity, IEntityWithIntKey
        {
            return items.GetAll().GetAllActiveSorted(id);
        }
        
        public static IList<TEntity> GetAllSorted<TEntity>(this IEntityCacheService<TEntity> items)
            where TEntity : class, INamedEntity, IGetIdentifier
        {
            return items.GetAll().GetAllSorted();
        }
    }
}
