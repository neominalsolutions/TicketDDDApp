using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainEvents.Src.SeedWork
{
    public interface IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
    }
}
