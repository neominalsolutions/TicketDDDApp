using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainEvents.Src.Domain.Tickets.Services
{
    /// <summary>
    /// Çalışana ticket atama işlemi yaparken belirli iş kurallarını kontrol etmek için açtığımız servis
    /// </summary>
    public interface ITicketAssignmentDomainService
    {
        /// <summary>
        /// Kime hangi ticket'ın atanacağının kontrolünü yapan method
        /// </summary>
        /// <param name="ticketNumber"></param>
        /// <param name="assignedTo"></param>
        void Assign(string ticketNumber, string assignedTo);
    }
}
