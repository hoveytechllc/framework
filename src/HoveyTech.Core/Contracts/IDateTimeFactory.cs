using System;

namespace HoveyTech.Core.Contracts
{
    public interface IDateTimeFactory
    {
        DateTimeOffset UtcNowWithOffset { get; }

        DateTimeOffset NowWithOffset { get; }

        DateTime UtcNow { get; }

        DateTime Now { get; }
    }
}
