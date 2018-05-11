using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using HoveyTech.Core.Data.Dapper.Contracts;
using HoveyTech.Core.Data.Dapper.Repository;
using Moq;
using Xunit;

namespace HoveyTech.Core.Data.Dapper.Tests.Transactions
{
    public class SqliteTransactionTests
    {
        [Fact]
        public void Connection_is_same_as_parent_when_child()
        {
            var context = new TestableSqliteTransaction();

            var parent = new SqliteTransaction(context.ConnectionStringFactory.Object, IsolationLevel.ReadCommitted);

            var child = new SqliteTransaction(parent);

            Assert.False(child.IsOwner);
            Assert.True(child.IsOpen);
            Assert.True(child.Connection == parent.Connection);
        }

        [Fact]
        public void ctor_does_create_open_connection()
        {
            var context = new TestableSqliteTransaction();

            var sut = new SqliteTransaction(context.ConnectionStringFactory.Object, IsolationLevel.ReadCommitted);

            Assert.NotNull(sut.Connection);
            Assert.Equal(ConnectionState.Open, sut.Connection.State);

            sut.Connection.Execute("CREATE TABLE [City] ([Id] INT NOT NULL PRIMARY KEY, [Name] TEXT NOT NULL)");
            sut.Connection.Execute("INSERT INTO [City] ([Id], [Name]) VALUES (1, 'Phoenix')");

            var cities = sut.Connection.Query<City>("SELECT * FROM [City]")
                .ToList();

            Assert.Single(cities);
            Assert.Equal("Phoenix", cities[0].Name);
            Assert.Equal(1, cities[0].Id);
        }
    }

    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class TestableSqliteTransaction
    {
        public Mock<IConnectionStringFactory> ConnectionStringFactory { get; }

        public TestableSqliteTransaction()
        {
            ConnectionStringFactory = new Mock<IConnectionStringFactory>();
            ConnectionStringFactory.Setup(x => x.Get()).Returns(() => "Data Source=:memory:");
        }
    }
}
