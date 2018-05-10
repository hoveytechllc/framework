namespace HoveyTech.Core.Data.EntityFrameworkCore.Contracts
{
    public interface IMobileDbContextFactory : IDbContextFactory
    {
        bool IsDatabasePresent();

        string GetFullPath();

        string GetFilename();
    }
}
