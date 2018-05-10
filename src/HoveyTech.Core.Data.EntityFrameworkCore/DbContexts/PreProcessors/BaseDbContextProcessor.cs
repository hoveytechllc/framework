using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HoveyTech.Core.Data.EntityFrameworkCore.DbContexts.PreProcessors
{
    public class BaseDbContextProcessor
    {
        protected bool IsAttachedAndNotUnChanged(EntityEntry entry)
        {
            return entry.State != EntityState.Detached
                   && entry.State != EntityState.Unchanged;
        }
    }
}
