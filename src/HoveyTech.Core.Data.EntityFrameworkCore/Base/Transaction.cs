using System.Data;
using HoveyTech.Core.Data.EntityFrameworkCore.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HoveyTech.Core.Data.EntityFrameworkCore.Base
{
    public class EntityFrameworkCoreTransaction : BaseTransaction<IDbTransaction>, IEntityFrameworkCoreTransaction
    {
        public IEntityFrameworkCoreTransaction ParentContext { get; protected set; }

        protected DbContext InnerContext;
        public virtual DbContext Context
        {
            get
            {
                if (ParentContext != null)
                    return ParentContext.Context;
                return InnerContext;
            }
        }

        /// <summary>
        /// Re-using another transaction. We will 'join it'
        /// and will not take responsibility to rollback or commit transaction
        /// </summary>
        /// <param name="transaction"></param>
        public EntityFrameworkCoreTransaction(IEntityFrameworkCoreTransaction transaction)
        {
            Parent = ParentContext = transaction;
        }

        /// <summary>
        /// This is a brand new connection to database.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="level"></param>
        public EntityFrameworkCoreTransaction(DbContext context, IsolationLevel? level = null)
        {
            InnerContext = context;

            if (level.HasValue)
                InnerContext.Database.BeginTransaction(level.Value);
            else
                InnerContext.Database.BeginTransaction();
        }

        public virtual DbSet<TEntity> Set<TEntity>()
            where TEntity : class
        {
            return Context.Set<TEntity>();
        }

        protected override void DisposeTransaction()
        {
            base.DisposeTransaction();

            InnerContext?.Dispose();
            InnerContext = null;
        }
    }
}
