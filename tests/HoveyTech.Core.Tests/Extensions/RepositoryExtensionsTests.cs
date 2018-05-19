using HoveyTech.Core.Contracts.Data;
using HoveyTech.Core.Contracts.Model;
using HoveyTech.Core.Extensions;
using HoveyTech.Core.Paging;
using HoveyTech.Core.Tests.Model;
using Moq;
using Xunit;

namespace HoveyTech.Core.Tests.Extensions
{
    public class RepositoryExtensionsTests
    {
        public class TestEntity : IStateAware
        {
            public int Id { get; set; }

            public TestEntity(int id)
            {
                Id = id;
            }

            public object GetIdentifier()
            {
                return null;
            }

            public bool IsNew => Id == 0;
        }

        [Fact]
        public void AddOrUpdate_does_call_update_when_entity_is_not_new()
        {
            var entity = new TestEntity(3);
            var tranMock = new Mock<ITransaction>();
            var repositoryMock = new Mock<IHasTransactionRepository<TestEntity, ITransaction>>();
            repositoryMock.Setup(x => x.GetTransaction()).Returns(() => tranMock.Object);
            repositoryMock.Object.AddOrUpdate(entity);

            repositoryMock.Verify(x => x.Update(entity));
            tranMock.Verify(x => x.CommitIfOwner());
        }

        [Fact]
        public void AddOrUpdate_does_call_add_when_entity_is_new()
        {
            var entity = new TestEntity(0);
            var tranMock = new Mock<ITransaction>();
            var repositoryMock = new Mock<IHasTransactionRepository<TestEntity>>();
            repositoryMock.Setup(x => x.GetTransaction()).Returns(() => tranMock.Object);
            repositoryMock.Object.AddOrUpdate(entity);

            repositoryMock.Verify(x => x.Add(entity));
            tranMock.Verify(x => x.CommitIfOwner());
        }

        [Fact]
        public void GetAllPagedByGuid_does_call_FindWithPaging_on_repository()
        {
            var request = new PagingRequest();
            var repositoryMock = new Mock<IPagingRepository<TestableBaseEntityWithGuidKey>>();
            repositoryMock.Object.GetAllPagedByGuid(request);

            repositoryMock.Verify(x => x.FindWithPaging(request, e => e.Id, null));
        }

        [Fact]
        public void GetAllPagedByInt_does_call_FindWithPaging_on_repository()
        {
            var request = new PagingRequest();
            var repositoryMock = new Mock<IPagingRepository<TestableBaseEntityWithIntKey>>();
            repositoryMock.Object.GetAllPagedByInt(request);

            repositoryMock.Verify(x => x.FindWithPaging(request, e => e.Id, null));
        }
    }
}
