using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using HoveyTech.Core.Contracts.Data;
using HoveyTech.Core.Contracts.Model;
using HoveyTech.Core.Data.Dapper.Contracts;

namespace HoveyTech.Core.Data.Dapper.Repository
{
    public class DapperRepository<TEntity> : IDapperRepository<TEntity>
        where TEntity : class, IGetIdentifier
    {
        private readonly IConnectionStringFactory _connectionStringFactory;

        public DapperRepository(IConnectionStringFactory connectionStringFactory)
        {
            _connectionStringFactory = connectionStringFactory;
        }
        
        private ISqlClientTransaction _currentTransaction;
        public ISqlClientTransaction CurrentTransaction => _currentTransaction?.IsOpen ?? false ? _currentTransaction : null;

        public void JoinTransaction(ISqlClientTransaction sqlClientTransaction)
        {
            _currentTransaction?.Dispose();
            _currentTransaction = sqlClientTransaction;
        }

        public virtual ISqlClientTransaction GetTransaction()
        {
            return GetTransaction(IsolationLevel.ReadCommitted);
        }

        public virtual ISqlClientTransaction GetTransaction(IsolationLevel level)
        {
            if (CurrentTransaction != null)
                return CurrentTransaction;

            _currentTransaction = new SqlClientTransaction(_connectionStringFactory, level);
            return _currentTransaction;
        }

        private string _tableName;
        public virtual string TableName => _tableName = _tableName ?? typeof(TEntity).Name;

        protected virtual dynamic Mapping(TEntity entity)
        {
            return entity;
        }

        public virtual TEntity GetById(object id)
        {
            using (var tran = GetTransaction())
            {
                var result = tran.Connection.Query<TEntity>(
                        $"SELECT * FROM {TableName} WHERE Id=@Id", new { Id = id },
                        transaction: tran.Transaction)
                        .SingleOrDefault();

                tran.CommitIfOwner();

                return result;
            }
        }

        public virtual TEntity Add(TEntity entity)
        {
            using (var tran = GetTransaction())
            {
                tran.Connection.Insert(entity, transaction: tran.Transaction);

                tran.CommitIfOwner();

                return entity;
            }
        }

        public virtual TEntity Update(TEntity entity)
        {
            using (var tran = GetTransaction())
            {
                tran.Connection.Update(entity, transaction: tran.Transaction);

                tran.CommitIfOwner();

                return entity;
            }
        }
        
        public virtual IList<TEntity> GetAll()
        {
            using (var tran = GetTransaction())
            {
                return tran.Connection.Query<TEntity>($"SELECT * FROM {TableName}",
                    transaction: tran.Transaction).ToList();
            }
        }
        
        public virtual void Delete(TEntity entity)
        {
            using (var tran = GetTransaction())
            {
                tran.Connection.Execute(
                    $"DELETE FROM {TableName} WHERE [Id] = @Id", new {Id = entity.GetIdentifier()},
                    transaction: tran.Transaction);

                tran.CommitIfOwner();
            }
        }
        
    }
}