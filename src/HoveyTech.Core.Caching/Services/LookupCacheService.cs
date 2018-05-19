using System;
using System.Collections.Generic;
using System.Linq;
using HoveyTech.Core.Contracts.Caching;
using HoveyTech.Core.Contracts.Data;
using HoveyTech.Core.Contracts.Model;
using HoveyTech.Core.Model;

namespace HoveyTech.Core.Caching.Services
{
    public class LookupCacheService<TEntity, TLookup, TTransaction> : ILookupCacheService<TEntity, TLookup, TTransaction>
        where TLookup : struct, IConvertible
        where TEntity : BaseEntityWithIntKey, INamedEntity
        where TTransaction : ITransaction
    {
        private readonly IEntityCacheService<TEntity, TTransaction> _cacheService;
        private IDictionary<TLookup, TEntity> _dictionary;

        public LookupCacheService(IEntityCacheService<TEntity, TTransaction> cacheService)
        {
            _cacheService = cacheService;
        }

        private IDictionary<TLookup, TEntity> GetDictionary()
        {
            if (_dictionary != null)
                return _dictionary;

            var dictionary = new Dictionary<TLookup, TEntity>();

            foreach (var entity in _cacheService.GetAll())
            {
                TLookup lookup;
                if (!Enum.TryParse(entity.Name, out lookup))
                    continue;
                
                dictionary.Add(lookup, entity);
            }

            _dictionary = dictionary;
            return _dictionary;
        }

        public TEntity GetEntityByLookup(TLookup lookup)
        {
            var dictionary = GetDictionary();

            TEntity entity;
            if (!dictionary.TryGetValue(lookup, out entity))
                throw new Exception($"Could not find entity of type {typeof(TEntity).Name} by lookup value {lookup}");

            return entity;
        }

        public TLookup GetLookupById(int id)
        {
            var items = GetDictionary()
                .Where(x => x.Value.Id == id)
                .ToList();
            
            if (!items.Any())
                throw new Exception($"Could not find entity of type {typeof(TEntity).Name} by id {id}");

            return items.First().Key;
        }

        public TLookup GetLookupByEntity(TEntity entity)
        {
            return GetLookupById(entity.Id);
        }

        public int GetIdByLookup(TLookup lookup)
        {
            return GetEntityByLookup(lookup).Id;
        }

        public TTransaction GetTransaction()
        {
            return _cacheService.GetTransaction();
        }

        public void JoinTransaction(TTransaction tran)
        {
            _cacheService.JoinTransaction(tran);
        }
    }
}
