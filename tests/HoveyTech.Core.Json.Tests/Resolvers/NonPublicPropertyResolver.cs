using System;
using Newtonsoft.Json;
using Xunit;

namespace HoveyTech.Core.Tests.Serialization
{
    public class NonPublicPropertyResolverTests
    {
        public class PrivateSetter
        {
            public string Property1 { get; private set; }

            public string Property2 { get; private set; }

            private PrivateSetter()
            {

            }

            public PrivateSetter(string property1)
            {
                Property1 = property1 ?? throw new ArgumentNullException(nameof(property1));
            }

            public static PrivateSetter Create(string value)
            {
                return new PrivateSetter(value);
            }
        }

        public class PublicAllTheWay
        {
            public string Property1 { get; set; }
        }

        [Fact]
        public void Serialize_does_set_camelCaseProperty_in_json_when_flag_true()
        {
            var json = JsonConvert.SerializeObject(new PrivateSetter("Value1"), new AzdaJsonSerializerSettings(camelCaseResolver: true));
            Assert.Contains("property1", json);
        }

        [Fact]
        public void Serialize_does_NOT_set_camelCaseProperty_in_json_when_flag_false()
        {
            var json = JsonConvert.SerializeObject(new PrivateSetter("Value1"), new AzdaJsonSerializerSettings());
            Assert.Contains("Property1", json);
        }

        [Fact]
        public void Deserialize_does_set_camelCaseProperty_in_json()
        {
            var read = JsonConvert.DeserializeObject<PrivateSetter>("{ \"property1\":\"Value1\"}", new AzdaJsonSerializerSettings());
            Assert.Equal("Value1", read.Property1);
        }

        [Fact]
        public void Deserialize_does_use_default_private_constructor()
        {
            var read = JsonConvert.DeserializeObject<PrivateSetter>("{}", new AzdaJsonSerializerSettings());
            Assert.NotNull(read);
            Assert.Null(read.Property1);
            Assert.Null(read.Property2);
        }

        [Fact]
        public void Deserialize_does_use_private_setter()
        {
            var read = JsonConvert.DeserializeObject<PrivateSetter>("{ \"Property1\":\"Value1\", \"Property2\":\"Value2\" }", new AzdaJsonSerializerSettings());
            Assert.Equal("Value1", read.Property1);
            Assert.Equal("Value2", read.Property2);
        }

        [Fact]
        public void Deserialize_does_use_POCO_setter()
        {
            var read = JsonConvert.DeserializeObject<PublicAllTheWay>("{ \"Property1\":\"Value1\" }", new AzdaJsonSerializerSettings());
            Assert.Equal("Value1", read.Property1);
        }
    }
}
