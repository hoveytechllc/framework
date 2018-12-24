using System.Data;
using HoveyTech.Core.Contracts.Model;
using HoveyTech.Core.Data.Dapper.Contracts;
using HoveyTech.Core.Data.Dapper.Repository;

namespace HoveyTech.Core.Data.Dapper.Repositories
{
    public class DapperSqlClientRepository<TEntity> : DapperRepository<TEntity>
        where TEntity : class, IGetIdentifier
    {
        public DapperSqlClientRepository(IConnectionStringFactory connectionStringFactory)
            : base(connectionStringFactory)
        {
        }

        public override IDapperTransaction CreateTransaction(IsolationLevel level)
        {
            return new SqlClientTransaction(ConnectionStringFactory, level);
        }

        protected override IDapperTransaction JoinTransactionInternal(IDapperTransaction tran)
        {
            return new SqlClientTransaction(tran as SqlClientTransaction);
        }
    }
}
