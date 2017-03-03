using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoveyTech.Core.Model;

namespace HoveyTech.Data.EfCore.Tests.TestContext
{
    public class TestGuidObject : BaseEntityWithGuidKey
    {
        public string Text { get; set; }

        public virtual IList<TestObject> TestObjects { get; set; }
    }
}
