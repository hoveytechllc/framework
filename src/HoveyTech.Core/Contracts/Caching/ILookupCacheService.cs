using System;
using HoveyTech.Core.Contracts.Data;

namespace HoveyTech.Core.Contracts.Caching
{
    public interface ILookupCacheService<TEntity, TLookup> : ILookupCacheService<TLookup>
        where TLookup : struct, IConvertible
        where TEntity : class
    {
        TEntity GetEntityByLookup(TLookup lookup);

        TLookup GetLookupByEntity(TEntity entity);
    }

    public interface ILookupCacheService<TLookup> : IHasTransaction
        where TLookup : struct , IConvertible
    {
        int GetIdByLookup(TLookup lookup);

        TLookup GetLookupById(int id);
    }
}
