using DomainEvents.Src.Domain.Tickets.Entities;
using DomainEvents.Src.Domain.Tickets.Events;
using DomainEvents.Src.EFCore;
using DomainEvents.Src.Infrastructure.Email;
using DomainEvents.Src.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DomainEvents.Src.Domain.Tickets.Handlers
{
    public class TicketOpenedHandler : INotificationHandler<DomainEventNotification<TicketOpened>>
    {
        private IEmailSender _emailSender;

        public TicketOpenedHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public System.Threading.Tasks.Task Handle(DomainEventNotification<TicketOpened> notification, CancellationToken cancellationToken)
        {
            var @event = notification.DomainEvent;

            _emailSender.SendEmail("nbuy.oglen@gmail.com", @event.OwnerEmailAddres, $"{@event.TicketNumber} numaralı Ticket {@event.OwnerName} adlı müşteri tarafından açıldı", "Open Ticket");



            return Task.CompletedTask;

            // ticket state attık
            // şimdi sıra emailde
        }
    }
}
