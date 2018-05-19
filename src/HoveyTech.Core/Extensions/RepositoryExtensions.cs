using HoveyTech.Core.Contracts.Data;
using HoveyTech.Core.Contracts.Model;
using HoveyTech.Core.Model;
using HoveyTech.Core.Paging;

namespace HoveyTech.Core.Extensions
{
    public static class RepositoryExtensions
    {
        public static TEntity AddOrUpdate<TEntity, TTransaction>(this IHasTransactionRepository<TEntity, TTransaction> repository, TEntity entity)
            where TEntity : class, IStateAware
            where TTransaction : ITransaction
        {
            using (var tran = repository.GetTransaction())
            {
                if (entity.IsNew)
                {
                    entity = repository.Add(entity);
                }
                else
                {
                    entity = repository.Update(entity);
                }

                tran.CommitIfOwner();

                return entity;
            }
        }

        public static IPagedList<TEntity> GetAllPagedByInt<TEntity>(this IPagingRepository<TEntity> repository, IPagingRequest request)
            where TEntity : BaseEntityWithIntKey
        {
            return repository.FindWithPaging(request, x => x.Id);
        }

        public static IPagedList<TEntity> GetAllPagedByGuid<TEntity>(this IPagingRepository<TEntity> repository, IPagingRequest request)
            where TEntity : BaseEntityWithGuidKey
        {
            return repository.FindWithPaging(request, x => x.Id);
        }
    }
}
