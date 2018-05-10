using HoveyTech.Core.Json.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HoveyTech.Core.Json.SerializerSettings
{
    public class HoveyTechDefaultJsonSerializerSettings : JsonSerializerSettings
    {
        public HoveyTechDefaultJsonSerializerSettings(bool camelCaseResolver = false)
        {
            IContractResolver resolver;

            if (camelCaseResolver)
                resolver = new NonPublicCamelCasePropertyResolver();
            else
                resolver = new NonPublicPropertyResolver();

            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
            ContractResolver = resolver;
        }
    }
}