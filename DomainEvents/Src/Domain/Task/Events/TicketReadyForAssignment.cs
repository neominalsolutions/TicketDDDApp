using DomainEvents.Src.Domain.Task.Entities;
using DomainEvents.Src.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainEvents.Src.Domain.Task.Events
{
    public class TicketReadyForAssignment: IDomainEvent
    {
        public string TicketId { get; private set; }
        public DateTime ReadyForAssignmentDate { get; set; }
        public TicketStates State { get; private set; }

        public TicketReadyForAssignment(string ticketId, DateTime date, TicketStates state)
        {
            ReadyForAssignmentDate = date;
            TicketId = ticketId;
            State = state;
        }

    }
}
