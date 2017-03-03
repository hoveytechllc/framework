using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HoveyTech.Data.EfCore.Tests.TestContext
{
    public class TestDbContextFactory : IDbContextFactory
    {
        public static bool Migrated;

        public DbContext Get()
        {
            var context = new TestDbContext();

            lock (this)
            {
                if (!Migrated)
                {
                    context.Database.Migrate();
                    Migrated = true;
                }
            }

            return context;
        }
    }

    public class TestDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={TestDbLocationProvider.GetTestFilePath()}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestObject>();
        }
    }
}
