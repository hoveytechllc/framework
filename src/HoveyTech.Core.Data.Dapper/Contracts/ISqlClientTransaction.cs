using System.Data.SqlClient;
using HoveyTech.Core.Contracts.Data;

namespace HoveyTech.Core.Data.Dapper.Contracts
{
    public interface ISqlClientTransaction : ITransaction
    {
        bool IsOpen { get; }

        SqlTransaction Transaction { get; }

        SqlConnection Connection { get; }
    }
}