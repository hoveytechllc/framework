using System.Data;
using HoveyTech.Core.Contracts.Data;
using HoveyTech.Core.Contracts.Model;

namespace HoveyTech.Core.Data.Dapper.Contracts
{
    public interface IDapperRepository<TEntity> : IHasTransactionRepository<TEntity, IDapperTransaction>
        where TEntity : class, IGetIdentifier
    {
    }
}
