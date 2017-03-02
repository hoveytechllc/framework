using Microsoft.EntityFrameworkCore;

namespace HoveyTech.Data.EfCore
{
    public interface IDbContextFactory
    {
        DbContext Get();
    }
}
