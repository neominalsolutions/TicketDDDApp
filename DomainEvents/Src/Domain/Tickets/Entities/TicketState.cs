using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainEvents.Src.Domain.Tickets.Entities
{
    public class TicketState
    {
        public string TicketId { get; private set; }
        public int State { get; private set; }
        public DateTime Date { get; private set; }
        public string Id { get; private set; }
        // tablo olması için unique Id ihtiyacı var

        public TicketState(string ticketId, int state, DateTime date)
        {
            Id = Guid.NewGuid().ToString();
            TicketId = ticketId;
            State = state;
            Date = date;
        }
    }
}
