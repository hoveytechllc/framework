using System;

namespace HoveyTech.Core.Contracts.Data
{
    public interface ITransaction : IDisposable
    {
        void CommitIfOwner();

        void Commit();

        void Rollback();

        bool IsOwner { get; }
    }
}
