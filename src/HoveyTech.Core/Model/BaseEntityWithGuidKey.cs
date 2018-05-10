using System;
using HoveyTech.Core.Contracts.Model;

namespace HoveyTech.Core.Model
{
    public class BaseEntityWithGuidKey : IEntityWithGuidKey
    {
        public virtual Guid Id { get; protected set; }

        public virtual bool IsNew => Id == Guid.Empty;

        public virtual void CreateIdentifier()
        {
            if (!IsNew) return;
            Id = Guid.NewGuid();
        }

        public virtual object GetIdentifier()
        {
            return Id;
        }
    }
}
