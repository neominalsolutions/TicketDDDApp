using DomainEvents.Src.Domain.Tickets.Events;
using DomainEvents.Src.SeedWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DomainEvents.Src.Domain.Tickets.Handlers
{
    public class TicketAssignedHandler : INotificationHandler<DomainEventNotification<TickedAssigned>>
    {
      

        public System.Threading.Tasks.Task Handle(DomainEventNotification<TickedAssigned> notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
