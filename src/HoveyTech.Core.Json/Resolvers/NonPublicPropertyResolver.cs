using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HoveyTech.Core.Json.Resolvers
{
    public class NonPublicCamelCasePropertyResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);

            var pi = member as PropertyInfo;
            if (pi != null)
                prop.Writable = (pi.SetMethod != null);

            return prop;
        }
    }

    public class NonPublicPropertyResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);

            var pi = member as PropertyInfo;
            if (pi != null)
                prop.Writable = (pi.SetMethod != null);

            return prop;
        }
    }

}
