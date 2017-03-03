using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HoveyTech.Core.Paging;

namespace HoveyTech.Core.Contracts.Data.Base
{
    public interface IPagingRepository<TEntity> : IPagingRepository<TEntity, IQueryableTransaction>
        where TEntity : class
    {
        
    }

    public interface IPagingRepository<TEntity, TTransaction> : IRepository<TEntity, TTransaction>
        where TEntity : class
        where TTransaction : IQueryableTransaction
    {
        IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        IPagedList<TEntity> FindWithPaging<TKey>(IPagingRequest pagingRequest,
            Expression<Func<TEntity, TKey>> orderBy,
            Expression<Func<TEntity, bool>> predicate = null);

        void UpdateRange(IEnumerable<TEntity> entities);

        void AddRange(IEnumerable<TEntity> entities);
    }
}
