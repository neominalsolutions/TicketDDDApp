using DomainEvents.Src.Domain.Task.Events;
using DomainEvents.Src.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DomainEvents.Src.Domain.Task.Handlers
{
    public class TicketOpenedHandler : INotificationHandler<DomainEventNotification<TicketOpened>>
    {

        private readonly ILogger<TicketOpenedHandler> _log;
        public TicketOpenedHandler(ILogger<TicketOpenedHandler> log)
        {
            _log = log;
        }

        public System.Threading.Tasks.Task Handle(DomainEventNotification<TicketOpened> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            try
            {
                _log.LogDebug($"Handling Domain Event. BacklogItemId:  Type:");
              
                return System.Threading.Tasks.Task.CompletedTask;
            }
            catch (Exception exc)
            {
                _log.LogError(exc, "Error handling domain event {domainEvent}", domainEvent.GetType());
                throw;
            }
        }
    }
}
