using HoveyTech.Core.Contracts.Data;

namespace HoveyTech.Core
{
    public abstract class BaseService : IService
    {
        protected abstract IHasTransaction[] ContextBasedMembers { get; }

        public void JoinTransaction(ITransaction tran)
        {
            JoinTransaction(tran, ContextBasedMembers);
        }

        public ITransaction GetTransaction()
        {
            return GetTransaction(ContextBasedMembers);
        }

        public static ITransaction GetTransaction(params IHasTransaction[] contextBasedMembers)
        {
            ITransaction tran = null;

            foreach (var contextBasedMember in contextBasedMembers)
            {
                if (tran == null)
                    tran = contextBasedMember.GetTransaction();

                contextBasedMember.JoinTransaction(tran);
            }

            return tran;
        }

        public static void JoinTransaction(ITransaction tran,
            params IHasTransaction[] contextBasedMembers)
        {
            foreach (var repository in contextBasedMembers)
            {
                repository.JoinTransaction(tran);
            }
        }
    }
}
