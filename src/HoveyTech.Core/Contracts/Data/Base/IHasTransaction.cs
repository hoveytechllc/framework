namespace HoveyTech.Core.Contracts.Data.Base
{
    /// <summary>
    /// Used by RepositoryBase. Repository that is specific to a DbContext
    /// </summary>
    public interface IHasTransaction
    {
        ITransaction GetTransaction();
        
        void JoinTransaction(ITransaction tran);
    }
}
