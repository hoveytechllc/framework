using System;

namespace HoveyTech.Core.Contracts.Model
{
    public interface ILastUpdated
    {
        DateTimeOffset LastUpdatedOn { get; set; }
    }
}
