using HoveyTech.Core.Contracts;
using HoveyTech.Core.Contracts.Model;
using HoveyTech.Core.Data.EntityFrameworkCore.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HoveyTech.Core.Data.EntityFrameworkCore.DbContexts.PreProcessors
{
    public class LastUpdatedDbContextPreProcessor : BaseDbContextProcessor, IDbContextPreProcessor
    {
        private readonly IDateTimeFactory _dateTimeFactory;

        public LastUpdatedDbContextPreProcessor(IDateTimeFactory dateTimeFactory)
        {
            _dateTimeFactory = dateTimeFactory;
        }

        public virtual void Run(DbContext dbContext)
        {
            foreach (var entry in dbContext.ChangeTracker.Entries())
            {
                if (!IsAttachedAndNotUnChanged(entry))
                    return;

                var lastUpdated = entry.Entity as ILastUpdated;
                lastUpdated?.MarkAsUpdated(_dateTimeFactory);
            }
        }

        protected virtual bool ProcessIsDirtyEntity(EntityEntry entry)
        {
            if (entry.Entity is IIsDirty isDirty
                && !isDirty.IsDirty
                && entry.State == EntityState.Modified)
            {
                entry.State = EntityState.Unchanged;
                return true;
            }

            return false;
        }
    }
}
