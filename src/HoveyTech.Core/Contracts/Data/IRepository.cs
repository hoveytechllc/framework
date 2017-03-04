using System.Collections.Generic;

namespace HoveyTech.Core.Contracts.Data
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        TEntity GetById(object id);

        IList<TEntity> GetAll();

        TEntity Add(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);
    }

}
