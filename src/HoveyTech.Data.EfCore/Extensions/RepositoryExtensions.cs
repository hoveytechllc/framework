using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoveyTech.Core.Contracts.Model;
using HoveyTech.Core.EfCore;

namespace HoveyTech.Data.EfCore.Extensions
{
    public static class RepositoryExtensions
    {
        public static TEntity Save<TEntity>(this IRepository<TEntity> repository, TEntity entity)
                where TEntity : class, IStateAware
        {
            using (var tran = (QueryableTransaction)repository.GetTransaction())
            {
                var set = tran.GetSet<TEntity>();

                if (entity.IsNew)
                    set.Add(entity);
                else
                    set.Update(entity);

                tran.CommitIfOwner();

                return entity;
            }
        }

    }
}
