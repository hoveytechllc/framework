using System.Threading.Tasks;
using HoveyTech.Core.Contracts.Data;

namespace HoveyTech.Core.Data.EntityFrameworkCore.Contracts
{
    public interface IModelBuilder : IHasTransaction
    {
        Task Initialize();
    }
}
