using HoveyTech.Core.Contracts.Data;
using HoveyTech.Core.Contracts.Model;

namespace HoveyTech.Core.Contracts.Caching
{
    public interface IEntityCacheService<TEntity, TTransaction> : IHasTransactionRepository<TEntity, TTransaction>
        where TEntity : class, IGetIdentifier
        where TTransaction : ITransaction
    {
        void ClearCache();
    }
}
