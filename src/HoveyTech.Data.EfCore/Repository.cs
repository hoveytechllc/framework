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
    public class Repository<TEntity> : IPagingRepository<TEntity>
      where TEntity : class
    {
        private readonly IDbContextFactory _dbContextFactory;

        public ITransaction CurrentTransaction { get; private set; }
        
        public Repository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public virtual ITransaction CreateContext(IsolationLevel level = IsolationLevel.ReadCommitted)
        {
            return new Transaction(_dbContextFactory.Get(), level);
        }

        public virtual bool HasOpenContext
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

        protected virtual TResult ExecuteWithTransaction<TResult>(Func<Transaction, TResult> func)
        {
            using (var tran = GetTransactionInternal())
            {
                var result = func(tran);

                tran.CommitIfOwner();

                return result;
            }
        }

        protected virtual void ExecuteWithTransaction(Action<Transaction> action)
        {
            using (var tran = GetTransactionInternal())
            {
                action(tran);

                tran.CommitIfOwner();
            }
        }
    }
}
