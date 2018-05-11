using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HoveyTech.Core.Data.Dapper.Contracts;

namespace HoveyTech.Core.Data.Dapper.Repository
{
    public class DapperTransaction : BaseTransaction<IDbTransaction>, IDapperTransaction
    {
        public IDbConnection Connection => Transaction.Connection;

        public DapperTransaction(DapperTransaction innerTran)
        {
            Parent = innerTran ?? throw new ArgumentNullException(nameof(innerTran));
        }

        protected DapperTransaction()
        {

        }

        protected void OpenConnectionAndBeginTransaction(IDbConnection conn, IsolationLevel level)
        {
            conn.Open();

            InnerTransaction = conn.BeginTransaction(level);
        }
    }
}
