using HoveyTech.Core.Contracts.Model;

namespace HoveyTech.Core.Model
{
    public class BaseLookupEntity : BaseEntityWithIntKey, IIsActive, INamedEntity
    {
        public BaseLookupEntity()
        {
            IsActive = true;
        }

        public virtual string Name { get; protected set; }
        
        public virtual bool IsActive { get; protected set; }
    }
}
