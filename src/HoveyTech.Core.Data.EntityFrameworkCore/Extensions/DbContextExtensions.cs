using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoveyTech.Core.Data.EntityFrameworkCore.Contracts;
using HoveyTech.Core.Data.EntityFrameworkCore.DbContexts.PreProcessors;
using Microsoft.EntityFrameworkCore;

namespace HoveyTech.Core.Data.EntityFrameworkCore.Extensions
{
    public static class DbContextExtensions
    {
        public static bool IsEntityIncludedInModel<TEntity>(this DbContext context)
        {
            var entityType = context.Model.FindEntityType(typeof(TEntity));
            return entityType != null;
        }

        public static async Task<int> RunPrePostProcessors<TDbContext>(this TDbContext dbContext, Func<Task<int>> saveChanges)
            where TDbContext : DbContext, IDbContextWithPrePostProcessing
        {
            var references = dbContext.RunPreProcessors();
            await saveChanges();
            dbContext.RunPostProcessors(references);
            return await saveChanges();
        }

        public static int RunPrePostProcessors<TDbContext>(this TDbContext dbContext, Func<int> saveChanges)
            where TDbContext : DbContext, IDbContextWithPrePostProcessing
        {
            var references = dbContext.RunPreProcessors();
            saveChanges();
            dbContext.RunPostProcessors(references);
            return saveChanges();
        }

        public static IList<EntityEntryReference> RunPreProcessors<TDbContext>(this TDbContext dbContext)
            where TDbContext : DbContext, IDbContextWithPrePostProcessing
        {
            if (dbContext == null)
                return null;
            if (dbContext.SuppressPrePostProcessing)
                return null;
            if (dbContext.PreProcessors == null)
                return null;

            foreach (var preProcessor in dbContext.PreProcessors)
                preProcessor.Run(dbContext);

            return dbContext.ChangeTracker
                .Entries()
                .Select(x => new EntityEntryReference(x))
                .ToList();
        }

        public static void RunPostProcessors<TDbContext>(this TDbContext dbContext,
            IList<EntityEntryReference> references)
            where TDbContext : DbContext, IDbContextWithPrePostProcessing
        {
            if (dbContext == null)
                return;
            if (dbContext.SuppressPrePostProcessing)
                return;
            if (dbContext.PostProcessors == null)
                return;

            foreach (var post in dbContext.PostProcessors)
                post.Run(references, dbContext);
        }

        public static void TrySuppressPrePostProcessing(this DbContext dbContext)
        {
            if (!(dbContext is IDbContextWithPrePostProcessing preprocessing))
                return;

            preprocessing.SuppressPrePostProcessing = true;
        }
    }
}
