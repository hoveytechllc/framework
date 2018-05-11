using HoveyTech.Core.Contracts.Data;

namespace HoveyTech.Core.Data.EntityFrameworkCore.Contracts
{
    public interface IPagingDbContextRepository<TEntity> : IPagingDbContextRepository<TEntity, IDbContextFactory>
        where TEntity : class
    {

    }

    public interface IPagingDbContextRepository<TEntity, TDbContextFactory> : IPagingRepository<TEntity, IEntityFrameworkCoreTransaction>
        where TEntity : class
        where TDbContextFactory : IDbContextFactory
    {
    }
}
