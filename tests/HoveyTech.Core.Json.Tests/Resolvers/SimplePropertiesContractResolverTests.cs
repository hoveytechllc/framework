using System;
using System.Collections.Generic;
using HoveyTech.Core.Json.Resolvers;
using HoveyTech.Core.Model;
using Newtonsoft.Json;
using Xunit;

namespace HoveyTech.Core.Json.Tests.Resolvers
{
    public class SimplePropertiesContractResolverTests
    {
        [Fact]
        public void Serialize_does_ignore_lookup_property_derived_from_BaseEntityWithGuidKey()
        {
            var obj = new TestSerializationObjectWithIntKey();

           var json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
            {
                ContractResolver = new SimplePropertiesContractResolver()
            });

            Assert.Equal($"{{\"IntProperty\":1,\"GuidProperty\":\"{obj.GuidProperty.ToString()}\"}}", json);
        }
    }

    public class TestSerializationObjectWithIntKey
    {
        public int IntProperty { get; set; }

        public Guid GuidProperty { get; set; }

        public TestChildObjectWithGuidKey GuidLookup { get; set; }

        public TestChildObjectWithIntKey IntLookup { get; set; }

        public IList<TestChildObjectWithGuidKey> GuidListLookup { get; set; }

        public IList<TestChildObjectWithIntKey> IntListLookup { get; set; }

        public TestSerializationObjectWithIntKey()
        {
            GuidListLookup = new List<TestChildObjectWithGuidKey>();
            IntListLookup = new List<TestChildObjectWithIntKey>();
            IntProperty = 1;
            GuidProperty = Guid.NewGuid();

            GuidLookup = new TestChildObjectWithGuidKey();
            IntLookup = new TestChildObjectWithIntKey();
        }
    }

    public class TestChildObjectWithIntKey : BaseEntityWithIntKey
    {

    }


    public class TestChildObjectWithGuidKey : BaseEntityWithGuidKey
    {

    }

}
