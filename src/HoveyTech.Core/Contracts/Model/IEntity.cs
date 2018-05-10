using System;
using System.Collections.Generic;
using System.Text;

namespace HoveyTech.Core.Contracts.Model
{
    public interface IEntity<out TKey> : IGetIdentifier
    {
        TKey Id { get; }
    }
}
