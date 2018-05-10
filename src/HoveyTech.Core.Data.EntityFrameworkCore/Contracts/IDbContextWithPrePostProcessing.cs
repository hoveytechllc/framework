using System.Collections.Generic;

namespace HoveyTech.Core.Data.EntityFrameworkCore.Contracts
{
    public interface IDbContextWithPrePostProcessing
    {
        bool SuppressPrePostProcessing { get; set; }

        IList<IDbContextPreProcessor> PreProcessors { get; }

        IList<IDbContextPostProcessor> PostProcessors { get; }
    }
}
