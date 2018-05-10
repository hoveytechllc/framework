using HoveyTech.Core.Contracts.Data;

namespace HoveyTech.Core.Data.EntityFrameworkCore.Contracts
{
    public interface IPagingDbContextRepository<TEntity, TDbContextFactory> : IPagingRepository<TEntity>
        where TEntity : class
        where TDbContextFactory : IDbContextFactory
    {
    }
}
