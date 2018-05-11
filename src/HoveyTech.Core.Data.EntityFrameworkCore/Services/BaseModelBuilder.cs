using System.Linq;
using System.Threading.Tasks;
using HoveyTech.Core.Contracts.Data;
using HoveyTech.Core.Data.EntityFrameworkCore.Base;
using HoveyTech.Core.Data.EntityFrameworkCore.Contracts;
using HoveyTech.Core.Data.EntityFrameworkCore.Extensions;
using HoveyTech.Core.Services;

namespace HoveyTech.Core.Data.EntityFrameworkCore.Services
{
    public abstract class BaseModelBuilder<TRepository, TEntity> 
        : BaseDataService<IHasTransaction<IEntityFrameworkCoreTransaction>, IEntityFrameworkCoreTransaction>, IModelBuilder
        where TRepository : IPagingDbContextRepository<TEntity>
        where TEntity : class
    {
        protected readonly TRepository Repository;

        protected BaseModelBuilder(TRepository repository)
        {
            Repository = repository;
        }

        protected virtual bool ForceInsert => false;

        public virtual async Task Initialize()
        {
            using (var tran = GetTransaction())
            {
                tran.TrySuppressPreprocessing();

                var anyRecords = tran.Context.Set<TEntity>().Any();

                if (!anyRecords || ForceInsert)
                    await InsertEntities();

                tran.CommitIfOwner();
            }
        }

        protected abstract Task InsertEntities();
    }

}
