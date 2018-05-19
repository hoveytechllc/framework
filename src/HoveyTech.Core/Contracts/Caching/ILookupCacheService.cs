using System;
using HoveyTech.Core.Contracts.Data;

namespace HoveyTech.Core.Contracts.Caching
{
    public interface ILookupCacheService<TEntity, TLookup, TTransaction> : ILookupCacheService<TLookup, TTransaction>
        where TLookup : struct, IConvertible
        where TEntity : class
        where TTransaction : ITransaction
    {
        TEntity GetEntityByLookup(TLookup lookup);

        TLookup GetLookupByEntity(TEntity entity);
    }

    public interface ILookupCacheService<TLookup, TTransaction> : IHasTransaction<TTransaction>
        where TLookup : struct , IConvertible
        where TTransaction : ITransaction
    {
        int GetIdByLookup(TLookup lookup);

        TLookup GetLookupById(int id);
    }
}
