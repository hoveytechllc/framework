using System.Collections.Generic;
using HoveyTech.Core.Contracts.Caching;
using HoveyTech.Core.Contracts.Data;
using HoveyTech.Core.Contracts.Model;

namespace HoveyTech.Core.Extensions
{
    public static class EntityCacheServiceExtensions
    { 
        public static IList<TEntity> GetAllActiveSorted<TEntity, TTransaction>(this IEntityCacheService<TEntity, TTransaction> items, int? id = null)
            where TEntity : class, IIsActive, INamedEntity, IEntityWithIntKey
            where TTransaction : ITransaction
        {
            return items.GetAll().GetAllActiveSorted(id);
        }
        
        public static IList<TEntity> GetAllSorted<TEntity, TTransaction>(this IEntityCacheService<TEntity, TTransaction> items)
            where TEntity : class, INamedEntity, IGetIdentifier
            where TTransaction : ITransaction
        {
            return items.GetAll().GetAllSorted();
        }
    }
}
