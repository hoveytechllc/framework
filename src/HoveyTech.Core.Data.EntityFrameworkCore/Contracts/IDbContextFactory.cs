using Microsoft.EntityFrameworkCore;

namespace HoveyTech.Core.Data.EntityFrameworkCore.Contracts
{
    public interface IDbContextFactory
    {
        DbContext Get();
    }
}
