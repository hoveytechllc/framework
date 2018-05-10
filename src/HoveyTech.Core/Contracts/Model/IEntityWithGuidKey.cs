using System;

namespace HoveyTech.Core.Contracts.Model
{
    public interface IEntityWithGuidKey : IEntity<Guid>, IIdentifierGenerator
    {

    }
}
