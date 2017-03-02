namespace HoveyTech.Core.Contracts.Model
{
    public interface IIdentifierGenerator : IStateAware
    {
        void CreateIdentifier();
    }
}
