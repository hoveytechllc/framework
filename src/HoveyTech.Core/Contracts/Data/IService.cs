namespace HoveyTech.Core.Contracts.Data
{
    public interface IDataService : IDataService<ITransaction>
    {

    }

    public interface IDataService<TTransaction> : IHasTransaction<TTransaction>
        where TTransaction : ITransaction
    {

    }
}
