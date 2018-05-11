using System;
using System.Data;
using HoveyTech.Core.Data.Dapper.Contracts;
using Microsoft.Data.Sqlite;

namespace HoveyTech.Core.Data.Dapper.Repository
{
    public class SqliteTransaction : DapperTransaction
    {
        public SqliteTransaction(IConnectionStringFactory connectionStringFactory, IsolationLevel level)
        {
            OpenConnectionAndBeginTransaction(new SqliteConnection(connectionStringFactory.Get()), level);
        }

        public SqliteTransaction(SqliteTransaction transaction)
            :base(transaction)
        {
            
        }
    }
}