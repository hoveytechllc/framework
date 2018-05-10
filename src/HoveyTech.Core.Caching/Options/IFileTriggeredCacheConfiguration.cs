using System;
using System.Collections.Generic;
using System.Text;

namespace HoveyTech.Core.Caching.Options
{
    public interface IFileTriggeredCacheConfiguration
    {
        string GetWatchDirectory();
    }
}
