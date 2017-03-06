using System;

namespace HoveyTech.Core.Contracts.Model
{
    public interface IEntityWithGuidKey
    {
        Guid Id { get; set; }
    }
}
