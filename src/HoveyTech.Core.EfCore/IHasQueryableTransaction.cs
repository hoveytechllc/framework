using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoveyTech.Core.Contracts.Data;

namespace HoveyTech.Core.EfCore
{
    public interface IHasQueryableTransaction : IHasTransaction<IQueryableTransaction>
    {
    }
}
