using System;
using System.Linq;

namespace HoveyTech.Core.Contracts.Data.Base
{
    public interface ITransaction : IDisposable
    {
        void CommitIfOwner();

        void Commit();

        void Rollback();

        bool IsOwner { get; }
    }

    public interface IQueryableTransaction : ITransaction
    {
        IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class;
    }
}
