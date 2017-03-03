using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoveyTech.Core.Contracts.Data;
using HoveyTech.Data.EfCore.Tests.TestContext;
using Xunit;

namespace HoveyTech.Data.EfCore.Tests
{
    public class RepositoryTests
    {
        private readonly IPagingRepository<TestObject, IQueryableTransaction> _sut;

        public RepositoryTests()
        {
            _sut = new Repository<TestObject>(new TestDbContextFactory());
        }

        [Fact]
        public void Add_does_set_id_property()
        {
            var result = _sut.Add(new TestObject()
            {
                Text = "Add_does_set_id_property",
                CreatedOn = DateTimeOffset.UtcNow
            });

            Assert.NotEqual(0, result.Id);
        }
    }
}
