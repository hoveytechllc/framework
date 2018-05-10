using HoveyTech.Core.Contracts.Model;

namespace HoveyTech.Core.Model
{
    public class BaseEntityWithIntKey : IStateAware, IGetIdentifier, IEntityWithIntKey
    {
        public virtual int Id { get; protected set; }

        public virtual bool IsNew => Id == 0;

        public virtual object GetIdentifier()
        {
            return Id;
        }
    }
}
