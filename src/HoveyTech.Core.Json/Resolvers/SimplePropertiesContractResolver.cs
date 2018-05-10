using System.Collections.Generic;
using System.Reflection;
using HoveyTech.Core.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HoveyTech.Core.Json.Resolvers
{
    public class SimplePropertiesContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);

            void Ignore()
            {
                prop.ShouldSerialize = obj => false;
                prop.Ignored = true;
            }

            var propType = prop.PropertyType.GetTypeInfo();

            if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(IList<>))
                Ignore();

            if (typeof(BaseEntityWithIntKey).GetTypeInfo().IsAssignableFrom(propType)
                || typeof(BaseEntityWithGuidKey).GetTypeInfo().IsAssignableFrom(propType))
                Ignore();

            return prop;
        }
    }
}
