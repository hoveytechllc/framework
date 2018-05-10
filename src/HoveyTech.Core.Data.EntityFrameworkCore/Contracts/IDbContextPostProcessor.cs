using System.Collections.Generic;
using HoveyTech.Core.Data.EntityFrameworkCore.DbContexts.PreProcessors;
using Microsoft.EntityFrameworkCore;

namespace HoveyTech.Core.Data.EntityFrameworkCore.Contracts
{
    public interface IDbContextPostProcessor
    {
        void Run(IList<EntityEntryReference> entryReferences, DbContext dbContext);
    }
}
