using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HoveyTech.Core.Data.EntityFrameworkCore.DbContexts.PreProcessors
{
    public class EntityEntryReference
    {
        public EntityState OriginalState { get; }
        
        public EntityEntry Entry { get; }

        public EntityEntryReference(EntityEntry entry)
        {
            Entry = entry;
            OriginalState = entry.State;
        }
    }
}
