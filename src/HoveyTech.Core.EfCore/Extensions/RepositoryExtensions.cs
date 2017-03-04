using System.Collections.Generic;
using System.Linq;
using HoveyTech.Core.Contracts.Data;
using HoveyTech.Core.Contracts.Model;
using HoveyTech.Core.Model;

namespace HoveyTech.Core.EfCore.Extensions
{
    public static class RepositoryExtensions
    {
        public static bool DoesEntityExistById<TEntity>(this IRepository<TEntity> repository, int id)
                where TEntity : BaseEntityWithIntKey
        {
            using (var tran = repository.GetTransaction())
            {
                var exists = tran.GetQueryable<TEntity>().Any(x => x.Id == id);

                tran.CommitIfOwner();

                return exists;
            }
        }

        public static IList<TEntity> GetAllActiveSorted<TEntity>(this IRepository<TEntity> repository, int? optionalId = null)
              where TEntity : BaseEntityWithIntKey, IIsActive, INamedEntity
        {
            using (var tran = repository.GetTransaction())
            {
                IQueryable<TEntity> query = tran.GetQueryable<TEntity>();

                if (optionalId.HasValue)
                    query = query.Where(x => x.IsActive || x.Id == optionalId.Value);
                else
                    query = query.Where(x => x.IsActive);

                query = query.OrderBy(x => x.Name);

                var results = query.ToList();

                tran.CommitIfOwner();

                return results;
            }
        }

        public static IList<TEntity> GetAllActive<TEntity>(this IRepository<TEntity> repository, int? optionalId = null)
            where TEntity : BaseEntityWithIntKey, IIsActive
        {
            using (var tran = repository.GetTransaction())
            {
                IQueryable<TEntity> query = tran.GetQueryable<TEntity>();

                if (optionalId.HasValue)
                    query = query.Where(x => x.IsActive || x.Id == optionalId.Value);
                else
                    query = query.Where(x => x.IsActive);

                var results = query.ToList();

                tran.CommitIfOwner();

                return results;
            }
        }

    }
}
