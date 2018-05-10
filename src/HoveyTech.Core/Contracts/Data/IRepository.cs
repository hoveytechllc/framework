using System.Collections.Generic;

namespace HoveyTech.Core.Contracts.Data
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        TEntity GetById(object id);

        IList<TEntity> GetAll();

        TEntity Add(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);
    }

    public interface IHasTransactionRepository<TEntity> : IHasTransactionRepository<TEntity, ITransaction>
        where TEntity : class
    {

    }
    
    public interface IHasTransactionRepository<TEntity, TTransaction> : IHasTransaction<TTransaction>
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
