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
    public class EntityCacheService<TEntity> : IEntityCacheService<TEntity>
        where TEntity : class, IGetIdentifier
    {
        private readonly IPagingRepository<TEntity> _repository;
        private readonly ICacheService<IList<TEntity>> _cacheService;

        public EntityCacheService(IPagingRepository<TEntity> repository,
            ICacheService<IList<TEntity>> cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
        }

        public void ClearCache()
        {
            _cacheService.Clear();
        }

        public ITransaction GetTransaction()
        {
            return _repository.GetTransaction();
        }

        public void JoinTransaction(ITransaction tran)
        {
            _repository.JoinTransaction(tran);
        }

        public ITransaction CurrentTransaction => _repository.CurrentTransaction;

        public IList<TEntity> GetAll()
        {
            return _cacheService.Get(_repository.GetAll);
        }

        public void Clear()
        {
            _cacheService.Clear();
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

        public IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll()
                .Where(predicate.Compile())
                .ToList();
        }

        public IPagedList<TEntity> FindWithPaging<TKey>(IPagingRequest pagingRequest,
            Expression<Func<TEntity, TKey>> orderBy,
            Expression<Func<TEntity, bool>> predicate = null)
        {
            var items = GetAll();

            if (predicate != null)
                items = items.Where(predicate.Compile()).ToList();

            return items.OrderBy(orderBy.Compile())
                .AsQueryable()
                .ToPagedList(pagingRequest);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _repository.UpdateRange(entities);
            _cacheService.Clear();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _repository.AddRange(entities);
            _cacheService.Clear();
        }
    }
}
