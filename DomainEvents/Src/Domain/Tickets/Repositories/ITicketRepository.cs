using DomainEvents.Src.Domain.Tickets.Entities;
using DomainEvents.Src.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainEvents.Src.Domain.Tickets.Repositories
{
    public interface ITicketRepository: IRepository<Ticket>
    {

    }
}
