using HoveyTech.Core.Contracts.Data;
using HoveyTech.Core.Data.EntityFrameworkCore.Base;

namespace HoveyTech.Core.Data.EntityFrameworkCore.Extensions
{
    public static class TransactionExtensions
    {
        public static ITransaction TrySuppressPreprocessing(this ITransaction tran)
        {
            if (!(tran is Transaction transaction))
                return tran;

            transaction.Context.TrySuppressPrePostProcessing();

            return tran;
        }
    }
}
