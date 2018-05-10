using System;
using System.Data;
using System.Data.SqlClient;
using HoveyTech.Core.Data.Dapper.Contracts;

namespace HoveyTech.Core.Data.Dapper.Repository
{
    public class SqlClientTransaction : ISqlClientTransaction
    {
        public ISqlClientTransaction ParentTransaction { get; protected set; }

        public bool IsOwner => ParentTransaction == null;

        public bool IsOpen => (Transaction?.Connection?.State ?? ConnectionState.Closed) == ConnectionState.Open;

        private SqlTransaction _transaction;
        public SqlTransaction Transaction
        {
            get
            {
                if (ParentTransaction != null)
                    return ParentTransaction.Transaction;
                return _transaction;
            }
        }

        public SqlConnection Connection => Transaction.Connection;
        
        public SqlClientTransaction(IConnectionStringFactory connectionStringFactory, IsolationLevel level)
        {
            var conn = new SqlConnection(connectionStringFactory.Get());
            conn.Open();

            _transaction = conn.BeginTransaction(level);
        }

        public SqlClientTransaction(ISqlClientTransaction innerTran)
        {
            ParentTransaction = innerTran ?? throw new ArgumentNullException(nameof(innerTran));
        }

        public void CommitIfOwner()
        {
            if (IsOwner)
                Commit();
        }

        public void Commit()
        {
            if (!IsOwner)
                throw new Exception("DbTransaction cannot commit transaction if not the owner of it.");

            Transaction.Commit();
            Transaction.Dispose();
            _transaction = null;
        }

        public void Dispose()
        {
            if (!IsOwner)
                return;

            Rollback();
        }

        public void Rollback()
        {
            if (!IsOwner)
                throw new Exception("Cannot Rollback transaction when not owner.");

            if (_transaction != null && _transaction.Connection != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
            }

            //if (_connection != null)
            //{
            //    if (_connection.State == ConnectionState.Open)
            //    {
            //        _connection.Close();
            //    }

            //    _connection.Dispose();
            //}

            //_connection = null;
            _transaction = null;
        }
    }
}