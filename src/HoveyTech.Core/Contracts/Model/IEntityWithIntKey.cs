namespace HoveyTech.Core.Contracts.Model
{
    public interface IEntityWithIntKey: IGetIdentifier
    {
        int Id { get; set; }
    }
}
