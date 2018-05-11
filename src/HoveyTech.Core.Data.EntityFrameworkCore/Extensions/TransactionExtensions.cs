using HoveyTech.Core.Data.EntityFrameworkCore.Contracts;

namespace HoveyTech.Core.Data.EntityFrameworkCore.Extensions
{
    public static class TransactionExtensions
    {
        public static IEntityFrameworkCoreTransaction TrySuppressPreprocessing(this IEntityFrameworkCoreTransaction tran)
        {
            tran.Context.TrySuppressPrePostProcessing();

            return tran;
        }
    }
}
