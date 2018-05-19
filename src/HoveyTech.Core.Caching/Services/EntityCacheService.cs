using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HoveyTech.Core.Contracts.Caching;
using HoveyTech.Core.Contracts.Data;
using HoveyTech.Core.Contracts.Model;
using HoveyTech.Core.Extensions;
using HoveyTech.Core.Paging;

namespace HoveyTech.Core.Caching.Services
{
    public class EntityCacheService<TEntity, TTransaction> : IEntityCacheService<TEntity, TTransaction>
        where TEntity : class, IGetIdentifier
        where TTransaction : ITransaction
    {
        private readonly IHasTransactionRepository<TEntity, TTransaction> _repository;
        private readonly ICacheService<IList<TEntity>> _cacheService;

        public EntityCacheService(IHasTransactionRepository<TEntity, TTransaction> repository,
            ICacheService<IList<TEntity>> cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
        }

        public void ClearCache()
        {
            _cacheService.Clear();
        }

        public TTransaction GetTransaction()
        {
            return _repository.GetTransaction();
        }

        public void JoinTransaction(TTransaction tran)
        {
            _repository.JoinTransaction(tran);
        }

        public TTransaction CurrentTransaction => _repository.CurrentTransaction;

        public IList<TEntity> GetAll()
        {
            return _cacheService.Get(_repository.GetAll);
        }
        
        public TEntity GetById(object id)
        {
            return GetAll()
                .FirstOrDefault(x => x.GetIdentifier().Equals(id));
        }

        public TEntity Add(TEntity entity)
        {
            entity = _repository.Add(entity);
            _cacheService.Clear();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            entity = _repository.Update(entity);
            _cacheService.Clear();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _repository.Delete(entity);
            _cacheService.Clear();
        }
    }
}
