using System;
using HoveyTech.Core.Contracts.Model;

namespace HoveyTech.Core.Model
{
    public class BaseEntityWithGuidKey : IIdentifierGenerator, IGetIdentifier
    {
        public virtual Guid Id { get; set; }

        public virtual bool IsNew => Id == Guid.Empty;

        public virtual void CreateIdentifier()
        {
            if (!IsNew) return;
            Id = Guid.NewGuid();
        }

        public object GetIdentifier()
        {
            return Id;
        }
    }
}
