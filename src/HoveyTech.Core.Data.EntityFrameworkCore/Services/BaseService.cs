using HoveyTech.Core.Contracts.Data;
using HoveyTech.Core.Data.EntityFrameworkCore.Base;

namespace HoveyTech.Core.Data.EntityFrameworkCore.Services
{
    public abstract class BaseDataService : IDataService
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
