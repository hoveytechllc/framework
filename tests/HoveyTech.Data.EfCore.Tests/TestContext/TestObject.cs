using System;
using HoveyTech.Core.Model;

namespace HoveyTech.Data.EfCore.Tests.TestContext
{
    public class TestObject : BaseEntityWithIntKey
    {
        public string Text { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public Guid TestGuidObjectId { get; set; }

        public virtual TestGuidObject TestGuidObject { get; set; }
    }
}
