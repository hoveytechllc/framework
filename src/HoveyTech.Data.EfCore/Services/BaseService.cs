using HoveyTech.Core.Contracts.Data.Base;

namespace HoveyTech.Data.EfCore.Services
{
    public abstract class BaseService : IService
    {
        public abstract IHasTransaction[] ContextBasedMembers { get; }

        public void JoinTransaction(ITransaction tran)
        {
            Transaction.JoinTransaction(tran, ContextBasedMembers);
        }

        public ITransaction GetTransaction()
        {
            return Transaction.GetTransaction(ContextBasedMembers);
        }
    }
}
