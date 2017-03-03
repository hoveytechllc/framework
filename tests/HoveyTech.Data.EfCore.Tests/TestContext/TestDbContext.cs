using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HoveyTech.Data.EfCore.Tests.TestContext
{
    public class TestDbContextFactory : IDbContextFactory
    {
        public string FilePath;

        public DbContext Get()
        {
            TestDbContext context;

            lock (this)
            {
                bool migrate = false;

                if (FilePath == null)
                {
                    FilePath = TestDbLocationProvider.GetTestFilePath();
                    migrate = true;
                }

                context = new TestDbContext(FilePath);

                if (migrate)
                    context.Database.Migrate();
            }

            return context;
        }
    }

    public class TestDbContext : DbContext
    {
        private readonly string _filePath;

        public TestDbContext(string filePath)
        {
            _filePath = filePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_filePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestObject>().HasOne(x => x.TestGuidObject).
                WithMany(x => x.TestObjects).
                HasForeignKey(x => x.TestGuidObjectId);
            modelBuilder.Entity<TestGuidObject>();
        }
    }
}
