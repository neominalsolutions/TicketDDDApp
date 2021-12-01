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
    public class TicketReadyForAssignmentHandler : INotificationHandler<DomainEventNotification<TicketReadyForAssignment>>
    {
        public System.Threading.Tasks.Task Handle(DomainEventNotification<TicketReadyForAssignment> notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
