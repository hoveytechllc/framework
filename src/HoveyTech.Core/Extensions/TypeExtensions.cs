using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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

            return false;
        }
    }
}
