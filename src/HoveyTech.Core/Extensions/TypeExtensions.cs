using System;
using System.Reflection;

namespace HoveyTech.Core.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsDerivedFromGeneric(this Type toCheck, Type generic)
        {
            foreach (var contract in toCheck.GetTypeInfo().ImplementedInterfaces)
            {
                if (contract.IsConstructedGenericType)
                {
                    var genericType = contract.GetGenericTypeDefinition();

                    if (genericType == generic)
                        return true;
                }
            }

            if (toCheck.GetTypeInfo().BaseType != null)
                return IsDerivedFromGeneric(toCheck.GetTypeInfo().BaseType, generic);

            return false;
        }
    }
}