using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HoveyTech.Core.Paging;

namespace HoveyTech.Core.Contracts.Data
{
    public interface IPagingRepository<TEntity> : IPagingRepository<TEntity, ITransaction>
        where TEntity : class
    {

    }

    public interface IPagingRepository<TEntity, TTransaction> : IHasTransactionRepository<TEntity, TTransaction>
        where TEntity : class
        where TTransaction : ITransaction
    {
        IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        IPagedList<TEntity> FindWithPaging<TKey>(IPagingRequest pagingRequest,
            Expression<Func<TEntity, TKey>> orderBy,
            Expression<Func<TEntity, bool>> predicate = null);

        void UpdateRange(IEnumerable<TEntity> entities);

        void AddRange(IEnumerable<TEntity> entities);
    }
}
