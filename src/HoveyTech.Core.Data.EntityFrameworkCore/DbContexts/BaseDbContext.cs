using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HoveyTech.Core.Contracts;
using HoveyTech.Core.Data.EntityFrameworkCore.Contracts;
using HoveyTech.Core.Data.EntityFrameworkCore.DbContexts.PreProcessors;
using HoveyTech.Core.Data.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HoveyTech.Core.Data.EntityFrameworkCore.DbContexts
{
    public class BaseDbContext : DbContext, IDbContextWithPrePostProcessing
    {
        private readonly IDateTimeFactory _dateTimeFactory;

        public BaseDbContext(IDateTimeFactory dateTimeFactory)
        {
            _dateTimeFactory = dateTimeFactory;
            PreProcessors = new List<IDbContextPreProcessor>();
            PostProcessors = new List<IDbContextPostProcessor>();
        }

        public IList<IDbContextPreProcessor> PreProcessors { get; }
        public IList<IDbContextPostProcessor> PostProcessors { get; }

        public bool SuppressPrePostProcessing { get; set; }

        protected virtual void AddDefaultPreProcessors()
        {
            PreProcessors.Add(new IsDirtyDbContextPreProcessor());
            PreProcessors.Add(new LastUpdatedDbContextPreProcessor(_dateTimeFactory));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            AddDefaultPreProcessors();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return this.RunPrePostProcessors(() => base.SaveChanges(acceptAllChangesOnSuccess));
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            return await this.RunPrePostProcessors(async () =>
                await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken));
        }
    }
}
