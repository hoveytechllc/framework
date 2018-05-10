namespace HoveyTech.Core.Contracts.Data
{
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
