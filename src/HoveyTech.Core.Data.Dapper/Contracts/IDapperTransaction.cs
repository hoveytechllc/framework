using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HoveyTech.Core.Data.Dapper.Contracts
{
    public interface IDapperTransaction : ITransaction<IDbTransaction>
    {
        IDbConnection Connection { get; }
    }
}
