using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HoveyTech.Core.Contracts.Model;
using HoveyTech.Core.Data.Dapper.Contracts;
using HoveyTech.Core.Data.Dapper.Repository;

namespace HoveyTech.Core.Data.Dapper.Repositories
{
    public class DapperSqliteRepository<TEntity> : DapperRepository<TEntity>
        where TEntity : class, IGetIdentifier
    {
        public DapperSqliteRepository(IConnectionStringFactory connectionStringFactory)
            : base(connectionStringFactory)
        {
        }

        public override IDapperTransaction CreateTransaction(IsolationLevel level)
        {
            return new SqliteTransaction(ConnectionStringFactory, level);
        }
    }
}
