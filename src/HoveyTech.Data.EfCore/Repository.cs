using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using HoveyTech.Core.Contracts.Data.Base;
using HoveyTech.Core.Contracts.Model;
using HoveyTech.Core.Paging;
using HoveyTech.Data.EfCore.Extensions;

namespace HoveyTech.Data.EfCore
{
    public class Repository<TEntity> : IPagingRepository<TEntity, IQueryableTransaction>
      where TEntity : class
    {
        private readonly IDbContextFactory _dbContextFactory;

        public IQueryableTransaction CurrentTransaction { get; private set; }
        
        public Repository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public virtual IQueryableTransaction CreateTransaction(IsolationLevel level = IsolationLevel.ReadCommitted)
        {
            return new QueryableTransaction(_dbContextFactory.Get(), level);
        }

        public virtual bool HasOpenTransaction
        {
            get
            {
                try
                {
                    var tran = (Transaction) CurrentTransaction;
                    return tran != null
                           && tran.IsOpen 
                           && tran.Context?.Database.CurrentTransaction != null;
                }
                catch (InvalidOperationException ex)
                {
                    if (ex.Message.Contains("the DbContext has been disposed"))
                        return false;
                    throw;
                }
            }
        }

        protected virtual QueryableTransaction GetTransactionInternal(IsolationLevel? level = null)
        {
            return (QueryableTransaction)GetTransactionWithIsolationLevel(level);
        }

        public virtual IQueryableTransaction GetTransaction()
        {
            return GetTransactionWithIsolationLevel();
        }

        public virtual IQueryableTransaction GetTransactionWithIsolationLevel(IsolationLevel? level = null)
        {
            if (HasOpenTransaction)
            {
                return new QueryableTransaction((Transaction)CurrentTransaction);
            }

            CurrentTransaction = new QueryableTransaction(_dbContextFactory.Get(), level);

            return CurrentTransaction;
        }

        public virtual void JoinTransaction(IQueryableTransaction tran)
        {
            CurrentTransaction = new QueryableTransaction((Transaction)tran);
        }

        public virtual TEntity GetById(object id)
        {
            return ExecuteWithTransaction(tran => tran.GetSet<TEntity>().Find(id));
        }

        public virtual IList<TEntity> GetAll()
        {
            return ExecuteWithTransaction(tran => tran.GetSet<TEntity>().ToList());
        }

        public virtual IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return ExecuteWithTransaction(tran => tran.GetSet<TEntity>().Where(predicate).ToList());
        }

        public virtual IPagedList<TEntity> FindWithPaging<TKey>(IPagingRequest pagingRequest, Expression<Func<TEntity, TKey>> orderBy, Expression<Func<TEntity, bool>> predicate = null)
        {
            return ExecuteWithTransaction(tran =>
            {
                IQueryable<TEntity> list = tran.GetSet<TEntity>();

                if (predicate != null)
                    list = list.Where(predicate);

                list = list.OrderBy(orderBy);

                return list.ToPagedList(pagingRequest);
            });
        }

        public virtual TEntity Add(TEntity entity)
        {
            (entity as IIdentifierGenerator)?.CreateIdentifier();
            ExecuteWithTransaction(tran => tran.GetSet<TEntity>().Add(entity));
            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            ExecuteWithTransaction(tran => tran.GetSet<TEntity>().Update(entity));
            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            ExecuteWithTransaction(tran => tran.GetSet<TEntity>().Remove(entity));
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            ExecuteWithTransaction(tran => tran.GetSet<TEntity>().UpdateRange(entities));
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            ExecuteWithTransaction(tran => tran.GetSet<TEntity>().AddRange(entities));
        }

        protected virtual TResult ExecuteWithTransaction<TResult>(Func<QueryableTransaction, TResult> func)
        {
            using (var tran = GetTransactionInternal())
            {
                var result = func(tran);

                tran.CommitIfOwner();

                return result;
            }
        }

        protected virtual void ExecuteWithTransaction(Action<QueryableTransaction> action)
        {
            using (var tran = GetTransactionInternal())
            {
                action(tran);

                tran.CommitIfOwner();
            }
        }
    }
}
