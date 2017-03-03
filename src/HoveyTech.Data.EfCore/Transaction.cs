using System;
using System.Data;
using HoveyTech.Core.Contracts.Data;
using Microsoft.EntityFrameworkCore;

namespace HoveyTech.Data.EfCore
{
    public class Transaction : ITransaction
    {
        public bool IsOwner { get; protected set; }

        public bool IsOpen => Context != null;

        public DbContext Context { get; protected set; }

        /// <summary>
        /// Re-using another transaction. We will 'join it'
        /// and will not take responsibility to rollback or commit transaction
        /// </summary>
        /// <param name="transaction"></param>
        public Transaction(Transaction transaction)
        {
            Context = transaction.Context;
            IsOwner = false;
        }

        /// <summary>
        /// This is a brand new connection to database.
        /// Consumer is responsible to commit or rollback.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="level"></param>
        public Transaction(DbContext context, IsolationLevel? level = null)
        {
            Context = context;

            if (level.HasValue)
                context.Database.BeginTransaction(level.Value);
            else
                Context.Database.BeginTransaction();

            IsOwner = true;
        }

        protected Transaction()
        {

        }

        public void CommitIfOwner()
        {
            if (IsOwner)
                Commit();
            else
                Context.SaveChanges();
        }

        public void Rollback()
        {
            if (!IsOwner)
            {
                return;
            }

            if (Context?.Database.CurrentTransaction == null)
            {
                return;
            }
            
            Context.Database.CurrentTransaction.Rollback();
            CloseConnection();
        }

        public void Commit()
        {
            if (!IsOwner)
                throw new Exception("DbTransaction cannot commit transaction if not the owner of it.");

            if (Context.Database.CurrentTransaction == null)
                throw new Exception("The DbContext does not have a CurrentContext.");

            Context.SaveChanges();
            Context.Database.CurrentTransaction.Commit();
            CloseConnection();
        }
        
        public void Dispose()
        {
            Rollback();
        }

        private void CloseConnection()
        {
            Context.Database.CloseConnection();
            Context.Dispose();
            Context = null;
        }
    }
}
