using DomainEvents.Src.Domain.Task.Entities;
using DomainEvents.Src.Domain.Task.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DomainEvents.Src.Domain.Task.Services
{
    public class TicketAssignmentDomainService : ITicketAssignmentDomainService
    {
        // repository bağlanmalıyız.

        private readonly ITicketRepository _ticketRepository;

        public TicketAssignmentDomainService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public void Assign(string ticketNumber, string assignedTo)
        {

            Func<Ticket, bool> monthlyFilter =
                (x => x.AssignedTo == assignedTo
                &&
                x.CurrentDate <= DateTime.Now.AddMonths(1));

            var ticket = _ticketRepository.Find(ticketNumber);

            var assignedHardLevelTickets = _ticketRepository
                .Where(x =>
                (
                    x.LevelOfDifficulty == (int)TicketDifficultyLevels.Hard
                    || 
                    x.LevelOfDifficulty == (int)TicketDifficultyLevels.Extreme
                ))
                .Where(monthlyFilter).ToList();

            var monthlyAssignedTasks = _ticketRepository.Where(
                x => x.AssignedTo == assignedTo
                &&
                x.CurrentDate <= DateTime.Now.AddMonths(1)
                );

            // aylık atanmış işler içerisindende zorluk derecesine göre hesaplama yaptık. 5 zorluk derecesine sahip bir iş 5 günlük bir iştir.
            var monthlyWorkingHours = monthlyAssignedTasks.Sum(x => x.LevelOfDifficulty * 8);

            var assignedTasksByLevelOfPriorityCount = _ticketRepository
                .Where(
                    x => x.LevelOfPriority == (int)TicketPriorityLevels.Critical
                )
                .Where(monthlyFilter)
                .Count();
                



            if (ticket == null)
            {
                throw new Exception("Atanacak bir ticket bulunamadı");
            }


            if (ticket.CurrentState != (int)TicketStates.ReadyForAssignment)
            {
                throw new Exception("Ticket atamak için uygun değildir.");
            }

            if (assignedHardLevelTickets.Count >= 3 || monthlyWorkingHours > 160)
            {
                throw new Exception("çalışana bu ay içerisinde çok fazla iş yüklendi");
            }

            if(assignedTasksByLevelOfPriorityCount >= 4)
            {
                throw new Exception("çalışana bu ay içerisinde çok fazla iş yüklendi");
            }


        }
    }
}
