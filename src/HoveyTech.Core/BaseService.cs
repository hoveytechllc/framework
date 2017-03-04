using HoveyTech.Core.Contracts.Data;

namespace HoveyTech.Core
{
    public abstract class BaseService<TTransaction> : IService<TTransaction>
        where TTransaction : class, ITransaction
    {
        protected abstract IHasTransaction<TTransaction>[] ContextBasedMembers { get; }

        public void JoinTransaction(TTransaction tran)
        {
            JoinTransaction(tran, ContextBasedMembers);
        }

        public TTransaction GetTransaction()
        {
            return GetTransaction(ContextBasedMembers);
        }

        public static TTransaction GetTransaction(params IHasTransaction<TTransaction>[] contextBasedMembers)
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
            params IHasTransaction<TTransaction>[] contextBasedMembers)
        {
            foreach (var repository in contextBasedMembers)
            {
                repository.JoinTransaction(tran);
            }
        }
    }
}
