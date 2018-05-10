using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HoveyTech.Core.Paging;

namespace HoveyTech.Core.Contracts.Data
{
    public interface IPagingRepository<TEntity> : IHasTransactionRepository<TEntity>
        where TEntity : class
    {
        IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        IPagedList<TEntity> FindWithPaging<TKey>(IPagingRequest pagingRequest,
            Expression<Func<TEntity, TKey>> orderBy,
            Expression<Func<TEntity, bool>> predicate = null);

        void UpdateRange(IEnumerable<TEntity> entities);

        void AddRange(IEnumerable<TEntity> entities);
    }
}
