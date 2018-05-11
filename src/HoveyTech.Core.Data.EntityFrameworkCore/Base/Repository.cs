using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using HoveyTech.Core.Contracts.Data;
using HoveyTech.Core.Data.EntityFrameworkCore.Contracts;
using HoveyTech.Core.Extensions;
using HoveyTech.Core.Paging;

namespace HoveyTech.Core.Data.EntityFrameworkCore.Base
{
    public class EntityFrameworkCoreRepository<TEntity> : EntityFrameworkCoreRepository<TEntity, IDbContextFactory>
        where TEntity : class
    {
        public EntityFrameworkCoreRepository(IDbContextFactory dbContextFactory) 
            : base(dbContextFactory)
        {
        }
    }

    public class EntityFrameworkCoreRepository<TEntity, TDbContextFactory> : IPagingDbContextRepository<TEntity, TDbContextFactory>
        where TEntity : class
        where TDbContextFactory : IDbContextFactory
    {
        private readonly TDbContextFactory _dbContextFactory;

        public IEntityFrameworkCoreTransaction CurrentTransaction { get; private set; }
        
        public EntityFrameworkCoreRepository(TDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public IEntityFrameworkCoreTransaction CreateContext(IsolationLevel level = IsolationLevel.ReadCommitted)
        {
            return new EntityFrameworkCoreTransaction(_dbContextFactory.Get(), level);
        }

        public bool HasOpenContext
        {
            get
            {
                try
                {
                    return CurrentTransaction?.IsOpen ?? false;
                }
                catch (InvalidOperationException ex)
                {
                    if (ex.Message.Contains("the DbContext has been disposed"))
                        return false;
                    throw;
                }
            }
        }

        protected virtual IEntityFrameworkCoreTransaction GetTransactionInternal(IsolationLevel? level = null)
        {
            return GetTransactionWithIsolationLevel(level);
        }

        public virtual IEntityFrameworkCoreTransaction GetTransaction()
        {
            return GetTransactionWithIsolationLevel();
        }

        public virtual IEntityFrameworkCoreTransaction GetTransactionWithIsolationLevel(IsolationLevel? level = null)
        {
            if (HasOpenContext)
            {
                return new EntityFrameworkCoreTransaction(CurrentTransaction);
            }

            CurrentTransaction = CreateContext(level ?? IsolationLevel.ReadCommitted);

            return CurrentTransaction;
        }

        public virtual void JoinTransaction(IEntityFrameworkCoreTransaction tran)
        {
            CurrentTransaction = new EntityFrameworkCoreTransaction(tran);
        }

        public virtual TEntity GetById(object id)
        {
            using (var tran = GetTransactionInternal())
            {
                var entity = tran.Set<TEntity>().Find(id);

                tran.CommitIfOwner();

                return entity;
            }
        }

        public virtual IList<TEntity> GetAll()
        {
            using (var tran = GetTransactionInternal())
            {
                var list = tran.Set<TEntity>().ToList();

                tran.CommitIfOwner();

                return list;
            }
        }

        public virtual IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            using (var tran = GetTransactionInternal())
            {
                var list = tran.Set<TEntity>().Where(predicate).ToList();

                tran.CommitIfOwner();

                return list;
            }
        }

        public virtual IPagedList<TEntity> FindWithPaging<TKey>(IPagingRequest pagingRequest, Expression<Func<TEntity, TKey>> orderBy, Expression<Func<TEntity, bool>> predicate = null)
        {
            using (var tran = GetTransactionInternal())
            {
                IQueryable<TEntity> list = tran.Set<TEntity>();

                if (predicate != null)
                    list = list.Where(predicate);

                list = list.OrderBy(orderBy);

                var pagedList = list.ToPagedList(pagingRequest);

                tran.CommitIfOwner();

                return pagedList;
            }
        }

        public virtual TEntity Add(TEntity entity)
        {
            using (var tran = GetTransactionInternal())
            {
                tran.Set<TEntity>().Add(entity);

                tran.CommitIfOwner();

                return entity;
            }
        }

        public virtual TEntity Update(TEntity entity)
        {
            using (var tran = GetTransactionInternal())
            {
                tran.Set<TEntity>().Update(entity);

                tran.CommitIfOwner();

                return entity;
            }
        }

        public virtual void Delete(TEntity entity)
        {
            using (var tran = GetTransactionInternal())
            {
                tran.Set<TEntity>().Remove(entity);

                tran.CommitIfOwner();
            }
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            using (var tran = GetTransactionInternal())
            {
                tran.Set<TEntity>().UpdateRange(entities);

                tran.CommitIfOwner();
            }
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            using (var tran = GetTransactionInternal())
            {
                tran.Set<TEntity>().AddRange(entities);

                tran.CommitIfOwner();
            }
        }
    }
}
