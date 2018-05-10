using Microsoft.EntityFrameworkCore;

namespace HoveyTech.Core.Data.EntityFrameworkCore.Contracts
{
    public interface IDbContextPreProcessor
    {
        void Run(DbContext dbContext);
    }
}