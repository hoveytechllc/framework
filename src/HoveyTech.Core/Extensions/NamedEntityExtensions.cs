using System.Collections.Generic;
using System.Linq;
using HoveyTech.Core.Contracts.Model;
using HoveyTech.Core.Model;

namespace HoveyTech.Core.Extensions
{
    public static class NamedEntityExtensions
    {
        public static string FindNameOfId<T>(this IEnumerable<T> list, int id)
            where T : BaseLookupEntity
        {
            var item = list.FirstOrDefault(x => x.Id == id);

            if (item == null) return "Unknown";
            return item.Name;
        }

        public static IEnumerable<T> FindByFilterText<T>(this IEnumerable<T> list, string filterText)
         where T : INamedEntity, IIsActive
        {
            return list.Where(x => x.IsActive &&
                        (filterText.IsNullOrEmpty() || x.Name.ToLower().Contains(filterText.ToLower()))).
                       OrderBy(x => x.Name.ToString());
        }
    }
}
