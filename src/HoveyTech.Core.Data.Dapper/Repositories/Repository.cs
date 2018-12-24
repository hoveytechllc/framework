using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using HoveyTech.Core.Contracts.Model;
using HoveyTech.Core.Data.Dapper.Contracts;

namespace HoveyTech.Core.Data.Dapper.Repository
{
    public abstract class DapperRepository<TEntity> : IDapperRepository<TEntity>
        where TEntity : class, IGetIdentifier
    {
        protected readonly IConnectionStringFactory ConnectionStringFactory;

        protected DapperRepository(IConnectionStringFactory connectionStringFactory)
        {
            ConnectionStringFactory = connectionStringFactory;
        }
        
        private IDapperTransaction _currentTransaction;
        public IDapperTransaction CurrentTransaction => _currentTransaction?.IsOpen ?? false ? _currentTransaction : null;

        public abstract IDapperTransaction CreateTransaction(IsolationLevel level);

        protected abstract IDapperTransaction JoinTransactionInternal(IDapperTransaction tran);

        public void JoinTransaction(IDapperTransaction sqlClientTransaction)
        {
            _currentTransaction?.Dispose();
            _currentTransaction =  sqlClientTransaction;
        }

        public virtual IDapperTransaction GetTransaction()
        {
            return GetTransaction(IsolationLevel.ReadCommitted);
        }

        public virtual IDapperTransaction GetTransaction(IsolationLevel level)
        {
            if (CurrentTransaction != null)
                return CurrentTransaction;
            
            _currentTransaction = CreateTransaction(level);
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