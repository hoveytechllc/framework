using System;
using System.Data;
using HoveyTech.Core.Contracts.Data;

namespace HoveyTech.Core.Data
{
    public interface ITransaction<out TSystemDataTransaction> : ITransaction
        where TSystemDataTransaction : IDbTransaction
    {
        ITransaction<TSystemDataTransaction> Parent { get; }

        TSystemDataTransaction Transaction { get; }
    }

    public abstract class BaseTransaction
    {
        public static ITransaction GetTransaction(params IHasTransaction[] contextBasedMembers)
        {
            return GetTransaction(null, contextBasedMembers);
        }

        public static ITransaction GetTransaction(IsolationLevel? level,
            params IHasTransaction[] contextBasedMembers)
        {
            ITransaction tran = null;

            foreach (var contextBasedMember in contextBasedMembers)
            {
                if (tran == null)
                    tran = contextBasedMember.GetTransaction();

                contextBasedMember.JoinTransaction(tran);
            }

            return tran;
        }

        public static void JoinTransaction(ITransaction tran,
            params IHasTransaction[] contextBasedMembers)
        {
            foreach (var repository in contextBasedMembers)
            {
                repository.JoinTransaction(tran);
            }
        }
    }

    public abstract class BaseTransaction<TSystemDataTransaction> : BaseTransaction, ITransaction<TSystemDataTransaction>
        where TSystemDataTransaction : IDbTransaction
    {
        public ITransaction<TSystemDataTransaction> Parent { get; protected set; }

        public virtual bool IsOwner => Parent == null;

        public virtual bool IsOpen => 
            (Transaction?.Connection?.State ?? ConnectionState.Closed) == ConnectionState.Open;

        protected TSystemDataTransaction InnerTransaction;
        public virtual TSystemDataTransaction Transaction
        {
            get
            {
                if (Parent != null)
                    return Parent.Transaction;
                return InnerTransaction;
            }
        }
        
        public virtual void CommitIfOwner()
        {
            if (IsOwner)
                Commit();
        }

        public virtual void Commit()
        {
            if (!IsOwner)
                throw new Exception("Cannot commit transaction if not the owner of it.");
            if (InnerTransaction == null)
                throw new Exception("Transaction is null while trying to commit.");
            
            InnerTransaction.Commit();

            DisposeTransaction();
        }

        public virtual void Dispose()
        {
            if (!IsOwner)
                return;

            Rollback();
        }

        public virtual void Rollback()
        {
            if (!IsOwner)
                throw new Exception("Cannot Rollback transaction when not owner.");
            
            InnerTransaction?.Rollback();

            DisposeTransaction();
        }

        protected virtual void DisposeTransaction()
        {
            if (InnerTransaction == null)
                return;

            InnerTransaction.Connection?.Close();
            InnerTransaction.Connection?.Dispose();
            InnerTransaction.Dispose();

            InnerTransaction = default(TSystemDataTransaction);
        }
    }
}