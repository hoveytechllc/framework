using System.Collections.Generic;
using System.Linq;
using HoveyTech.Core.Contracts.Model;

namespace HoveyTech.Core.Extensions
{
    public static class ListExtensions
    {
        public static IList<TEntity> GetAllSorted<TEntity>(this IEnumerable<TEntity> items)
            where TEntity : INamedEntity
        {
            return items.OrderBy(x => x.Name)
                .ToList();
        }

        public static IList<TEntity> GetAllActiveSorted<TEntity>(this IEnumerable<TEntity> items, int? id = null)
            where TEntity : IIsActive, INamedEntity, IEntityWithIntKey
        {
            return items.Where(x => (x.IsActive || (id.HasValue && id == x.Id)))
                .OrderBy(x => x.Name)
                .ToList();
        }
    }
}
