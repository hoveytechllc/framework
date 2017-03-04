using System;
using HoveyTech.Core.Contracts.Data;
using HoveyTech.Data.EfCore.Tests.TestContext;
using Xunit;

namespace HoveyTech.Data.EfCore.Tests
{
    public class RepositoryTests
    {
        private readonly IRepository<TestObject> _sut;
        private readonly IRepository<TestGuidObject> _testGuidObjectRepository;

        public RepositoryTests()
        {
            var dbContextFactory = new TestDbContextFactory();
            _testGuidObjectRepository = new Repository<TestGuidObject>(dbContextFactory);

            _sut = new Repository<TestObject>(dbContextFactory);
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

        [Fact]
        public void Add_does_respect_guid_foreign_key()
        {
            var testGuidObject = _testGuidObjectRepository.Add(new TestGuidObject()
            {
                Text = "Hello"
            });

            var result = _sut.Add(new TestObject()
            {
                Text = "Add_does_set_id_property",
                CreatedOn = DateTimeOffset.UtcNow,
                TestGuidObjectId = testGuidObject.Id
            });

            Assert.NotEqual(0, result.Id);
            Assert.Equal(result.TestGuidObjectId, testGuidObject.Id);
        }
    }
}
