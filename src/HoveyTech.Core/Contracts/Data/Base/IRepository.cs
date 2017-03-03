using System.Collections.Generic;

namespace HoveyTech.Core.Contracts.Data.Base
{
    public interface IRepository<TEntity> : IRepository<TEntity, ITransaction>
        where TEntity : class
    {

    }

    public interface ISetRepository<TEntity> : IRepository<TEntity, IQueryableTransaction>
        where TEntity : class
    {

    }

    public interface IRepository<TEntity, TTransaction> : IHasTransaction<TTransaction>
        where TEntity : class
        where TTransaction : ITransaction
    {
        TTransaction CurrentTransaction { get; }

        TEntity GetById(object id);

        IList<TEntity> GetAll();

        TEntity Add(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);
    }

}
