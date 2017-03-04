using System.Linq;
using HoveyTech.Core.Contracts.Data;

namespace HoveyTech.Core.EfCore
{
    public interface IQueryableTransaction : ITransaction
    {
        IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class;
    }
}