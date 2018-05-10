using HoveyTech.Core.Contracts.Model;
using HoveyTech.Core.Data.EntityFrameworkCore.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HoveyTech.Core.Data.EntityFrameworkCore.DbContexts.PreProcessors
{
    public class IsDirtyDbContextPreProcessor : IDbContextPreProcessor
    {
        public virtual void Run(DbContext dbContext)
        {
            foreach (var entry in dbContext.ChangeTracker.Entries())
            {
                if (ProcessIsDirtyEntity(entry))
                {
                    Run(dbContext);
                    return;
                }
            }
        }

        protected virtual bool ProcessIsDirtyEntity(EntityEntry entry)
        {
            if (entry.Entity is IIsDirty isDirty && !isDirty.IsDirty
                && entry.State == EntityState.Modified)
            {
                entry.State = EntityState.Unchanged;
                return true;
            }

            return false;
        }
    }
}
