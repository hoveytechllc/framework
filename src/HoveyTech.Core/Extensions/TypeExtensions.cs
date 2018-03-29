using System;
using System.Reflection;

namespace HoveyTech.Core.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsDerivedFromGeneric(this Type toCheck, Type generic)
        {
#if NETSTANDARD1_1
            foreach (var contract in toCheck.GetTypeInfo().ImplementedInterfaces)
            {
                if (contract.IsConstructedGenericType)
                {
                    var genericType = contract.GetGenericTypeDefinition();

                    if (genericType == generic)
                        return true;
                }
            }

            return false;
#else
            throw new NotImplementedException();
#endif
        }
    }
}