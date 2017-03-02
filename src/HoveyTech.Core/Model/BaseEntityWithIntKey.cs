using HoveyTech.Core.Contracts.Model;

namespace HoveyTech.Core.Model
{
    public class BaseEntityWithIntKey : IStateAware, IGetIdentifier
    {
        public virtual int Id { get; set; }

        public virtual bool IsNew => Id == 0;

        public object GetIdentifier()
        {
            return Id;
        }
    }
}
