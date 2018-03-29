using System;

namespace HoveyTech.Core.Contracts.Model
{
    public interface IEntityWithGuidKey : IGetIdentifier
    {
        Guid Id { get; set; }
    }
}
