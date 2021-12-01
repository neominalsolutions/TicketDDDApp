using DomainEvents.Src.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainEvents.Src.Domain.Tickets.Events
{
    public class TickedAssigned: IDomainEvent
    {
        public string TicketId { get; private set; }
        public string AssignTo { get; private set; }

        public DateTime AssignedDate { get; private set; }


        public TickedAssigned(string tickedId,string assignTo, DateTime assignedDate)
        {
            TicketId = tickedId;
            AssignTo = assignTo;
            AssignedDate = assignedDate;
        }
    }
}
