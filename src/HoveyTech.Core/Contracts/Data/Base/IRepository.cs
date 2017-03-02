using System.Collections.Generic;

namespace HoveyTech.Core.Contracts.Data.Base
{
    public interface IRepository<TEntity> : IHasTransaction
        where TEntity : class
    {
        ITransaction CurrentTransaction { get; }

        TEntity GetById(object id);

        IList<TEntity> GetAll();

        TEntity Add(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
