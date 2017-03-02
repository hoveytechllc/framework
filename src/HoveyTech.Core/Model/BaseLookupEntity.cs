using HoveyTech.Core.Contracts.Model;

namespace HoveyTech.Core.Model
{
    public class BaseLookupEntity : BaseEntityWithIntKey, IIsActive, INamedEntity
    {
        public string Name { get; set; }
        
        public bool IsActive { get; set; }
    }
}
