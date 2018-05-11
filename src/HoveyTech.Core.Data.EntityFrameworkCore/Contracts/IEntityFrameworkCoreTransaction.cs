using System.Data;
using Microsoft.EntityFrameworkCore;

namespace HoveyTech.Core.Data.EntityFrameworkCore.Contracts
{
    public interface IEntityFrameworkCoreTransaction : ITransaction<IDbTransaction>
    {
        DbContext Context { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
