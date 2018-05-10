using System;
using Microsoft.EntityFrameworkCore;

namespace HoveyTech.Core.Data.EntityFrameworkCore.Extensions
{
    public static class EntityStateExtensions
    {
        public const string DeletedOperation = "D";
        public const string UpdatedOperation = "U";
        public const string InsertedOperation = "I";
        
        public static string GetOperationText(this EntityState state)
        {
            switch (state)
            {
                case EntityState.Added:
                    return InsertedOperation;
                case EntityState.Deleted:
                    return DeletedOperation;
                case EntityState.Modified:
                    return UpdatedOperation;
                default:
                    throw new Exception($"Operation not supported in DbContext, {state}");
            }
        }
    }
}
