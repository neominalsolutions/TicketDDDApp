using MediatR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainEvents.Src.SeedWork
{
    public abstract class Entity: IEntity
    {
        //...
        private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        // sınıfa atadığımız event
        public void AddEvents(IDomainEvent @event)
        {
            _domainEvents.Add(@event);
        }

    }
}
