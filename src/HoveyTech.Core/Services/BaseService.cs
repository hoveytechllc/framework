using HoveyTech.Core.Contracts.Data;

namespace HoveyTech.Core.Services
{
    public abstract class BaseService<THasTransaction, TTransaction> : IService<TTransaction>
        where TTransaction : class, ITransaction
        where THasTransaction : IHasTransaction<TTransaction>
    {
        protected abstract THasTransaction[] ContextBasedMembers { get; }

        public void JoinTransaction(TTransaction tran)
        {
            JoinTransaction(tran, ContextBasedMembers);
        }

        public TTransaction GetTransaction()
        {
            return GetTransaction(ContextBasedMembers);
        }

        public static TTransaction GetTransaction(params THasTransaction[] contextBasedMembers)
        {
            TTransaction tran = null;

            foreach (var contextBasedMember in contextBasedMembers)
            {
                if (tran == null)
                    tran = contextBasedMember.GetTransaction();

                contextBasedMember.JoinTransaction(tran);
            }

            return tran;
        }

        public static void JoinTransaction(TTransaction tran,
            params THasTransaction[] contextBasedMembers)
        {
            foreach (var repository in contextBasedMembers)
            {
                repository.JoinTransaction(tran);
            }
        }
    }
}
