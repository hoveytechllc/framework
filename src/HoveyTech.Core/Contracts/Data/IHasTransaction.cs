namespace HoveyTech.Core.Contracts.Data
{
    /// <summary>
    /// Used by RepositoryBase. Repository that is specific to a DbContext
    /// </summary>
    public interface IHasTransaction : IHasTransaction<ITransaction>
    {

    }

    public interface IHasTransaction<TTransaction>
        where TTransaction : ITransaction
    {
        TTransaction GetTransaction();

        void JoinTransaction(TTransaction tran);
    }
}
