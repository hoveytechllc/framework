using System;

namespace HoveyTech.Core.Contracts.Data.Base
{
    public interface ITransaction : IDisposable
    {
        void CommitIfOwner();

        void Commit();

        void Rollback();

        bool IsOwner { get; }
    }
}
