using System;

namespace HoveyTech.Core.Contracts.Model
{
    public interface ILastUpdated
    {
        DateTimeOffset LastUpdatedOn { get; }

        void MarkAsUpdated(IDateTimeFactory factory);
    }
}
