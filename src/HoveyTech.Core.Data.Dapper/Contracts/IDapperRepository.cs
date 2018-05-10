using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using HoveyTech.Core.Contracts.Data;
using HoveyTech.Core.Contracts.Model;

namespace HoveyTech.Core.Data.Dapper.Contracts
{
    public interface IDapperRepository<TEntity> : IHasTransactionRepository<TEntity, ISqlClientTransaction>
        where TEntity : class, IGetIdentifier
    {
    }
}
