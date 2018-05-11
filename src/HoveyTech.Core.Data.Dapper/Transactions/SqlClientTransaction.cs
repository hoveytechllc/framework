using System;
using System.Data;
using System.Data.SqlClient;
using HoveyTech.Core.Data.Dapper.Contracts;

namespace HoveyTech.Core.Data.Dapper.Repository
{
    public class SqlClientTransaction : DapperTransaction
    {
        public SqlClientTransaction(IConnectionStringFactory connectionStringFactory, IsolationLevel level)
        {
            OpenConnectionAndBeginTransaction(new SqlConnection(connectionStringFactory.Get()), level);
        }

        public SqlClientTransaction(SqlClientTransaction transaction)
            : base(transaction)
        {

        }
    }
}