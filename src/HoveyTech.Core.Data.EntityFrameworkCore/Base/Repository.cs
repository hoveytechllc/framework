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
    public class Repository<TEntity> : Repository<TEntity, IDbContextFactory>
        where TEntity : class
    {
        public Repository(IDbContextFactory dbContextFactory) 
            : base(dbContextFactory)
        {
        }
    }

    public class Repository<TEntity, TDbContextFactory> : IPagingDbContextRepository<TEntity, TDbContextFactory>
        where TEntity : class
        where TDbContextFactory : IDbContextFactory
    {
        private readonly TDbContextFactory _dbContextFactory;

        public ITransaction CurrentTransaction { get; private set; }
        
        public Repository(TDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public ITransaction CreateContext(IsolationLevel level = IsolationLevel.ReadCommitted)
        {
            return new Transaction(_dbContextFactory.Get(), level);
        }

        public bool HasOpenContext
        {
            get
            {
                try
                {
                    var tran = (Transaction) CurrentTransaction;
                    return tran != null && tran.IsOpen;
                }
                catch (InvalidOperationException ex)
                {
                    if (ex.Message.Contains("the DbContext has been disposed"))
                        return false;
                    throw;
                }
            }
        }

        protected virtual Transaction GetTransactionInternal(IsolationLevel? level = null)
        {
            return (Transaction)GetTransactionWithIsolationLevel(level);
        }

        public virtual ITransaction GetTransaction()
        {
            return GetTransactionWithIsolationLevel();
        }

        public virtual ITransaction GetTransactionWithIsolationLevel(IsolationLevel? level = null)
        {
            if (HasOpenContext)
            {
                return new Transaction((Transaction)CurrentTransaction);
            }

            CurrentTransaction = new Transaction(_dbContextFactory.Get(), level);

            return CurrentTransaction;
        }

        public virtual void JoinTransaction(ITransaction tran)
        {
            CurrentTransaction = new Transaction((Transaction)tran);
        }

        public virtual TEntity GetById(object id)
        {
            using (var tran = GetTransactionInternal())
            {
                var entity = tran.GetSet<TEntity>().Find(id);

                tran.CommitIfOwner();

                return entity;
            }
        }

        public virtual IList<TEntity> GetAll()
        {
            using (var tran = GetTransactionInternal())
            {
                var list = tran.GetSet<TEntity>().ToList();

                tran.CommitIfOwner();

                return list;
            }
        }

        public virtual IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            using (var tran = GetTransactionInternal())
            {
                var list = tran.GetSet<TEntity>().Where(predicate).ToList();

                tran.CommitIfOwner();

                return list;
            }
        }

        public virtual IPagedList<TEntity> FindWithPaging<TKey>(IPagingRequest pagingRequest, Expression<Func<TEntity, TKey>> orderBy, Expression<Func<TEntity, bool>> predicate = null)
        {
            using (var tran = GetTransactionInternal())
            {
                IQueryable<TEntity> list = tran.GetSet<TEntity>();

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
                tran.GetSet<TEntity>().Add(entity);

                tran.CommitIfOwner();

                return entity;
            }
        }

        public virtual TEntity Update(TEntity entity)
        {
            using (var tran = GetTransactionInternal())
            {
                tran.GetSet<TEntity>().Update(entity);

                tran.CommitIfOwner();

                return entity;
            }
        }

        public virtual void Delete(TEntity entity)
        {
            using (var tran = GetTransactionInternal())
            {
                tran.GetSet<TEntity>().Remove(entity);

                tran.CommitIfOwner();
            }
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            using (var tran = GetTransactionInternal())
            {
                tran.GetSet<TEntity>().UpdateRange(entities);

                tran.CommitIfOwner();
            }
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            using (var tran = GetTransactionInternal())
            {
                tran.GetSet<TEntity>().AddRange(entities);

                tran.CommitIfOwner();
            }
        }
    }
}
