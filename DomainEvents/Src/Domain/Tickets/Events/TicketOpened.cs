using DomainEvents.Src.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainEvents.Src.Domain.Tickets.Events
{
    public class TicketOpened: IDomainEvent
    {
        public string OwnerEmailAddres { get; private set; }
        public string OwnerName { get; private set; }
        public string TicketNumber { get; private set; }
        public DateTime OpenedDate { get; private set; }
        public Entities.TicketStates State { get; private set; } = Entities.TicketStates.Opened;
        public string Subject { get; private set; }
        public string Description { get; private set; }

        public string ManagerEmailAddress { get; set; }


        public TicketOpened(string subject, string description, string ticketNumber,string ownerEmailAddress, DateTime openedDate, string ownerName)
        {
            OwnerEmailAddres = ownerEmailAddress;
            Subject = subject;
            Description = description;
            OpenedDate = openedDate;
            TicketNumber = ticketNumber;
            OwnerName = ownerName;
        }


        public TicketOpened(string subject, string description, string ticketNumber, string ownerEmailAddress, DateTime openedDate, string ownerName, string managerEmailAddress):this(subject,description,ticketNumber,ownerEmailAddress,openedDate,ownerName)
        {
            ManagerEmailAddress = managerEmailAddress;
        }



    }
}
