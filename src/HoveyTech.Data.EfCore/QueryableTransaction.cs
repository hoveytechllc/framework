using System.Data;
using System.Linq;
using HoveyTech.Core.EfCore;
using Microsoft.EntityFrameworkCore;

namespace HoveyTech.Data.EfCore
{
    public class QueryableTransaction : Transaction, IQueryableTransaction
    {
        public QueryableTransaction(QueryableTransaction transaction) 
            : base(transaction)
        {
        }

        public QueryableTransaction(DbContext context, IsolationLevel? level = null) 
            : base(context, level)
        {
        }
        
        public virtual IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class
        {
            return GetSet<TEntity>();
        }

        public virtual DbSet<TEntity> GetSet<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }
    }
}
