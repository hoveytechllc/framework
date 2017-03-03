using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoveyTech.Core.Contracts.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace HoveyTech.Data.EfCore
{
    public class QueryableTransaction : Transaction, IQueryableTransaction
    {
        public QueryableTransaction(Transaction transaction) 
            : base(transaction)
        {
        }

        public QueryableTransaction(DbContext context, IsolationLevel? level = null) 
            : base(context, level)
        {
        }
        
        public IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class
        {
            return GetSet<TEntity>();
        }

        public DbSet<TEntity> GetSet<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }
    }
}
