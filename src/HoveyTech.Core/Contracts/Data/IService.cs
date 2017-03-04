namespace HoveyTech.Core.Contracts.Data
{
    public interface IService<TTransaction> : IHasTransaction<TTransaction>
        where TTransaction : ITransaction
    {

    }
}
