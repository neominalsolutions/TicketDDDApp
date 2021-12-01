using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainEvents.Migrations;
using DomainEvents.Src.Domain.Tickets.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomainEvents.Pages.Tickets
{
    public class OpenTicketViewModel
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime OpenDate { get; set; }
        public string Key { get; set; }


    }

    public class OpenTicketsModel : PageModel
    {
        private readonly ITicketRepository _ticketRepo;

        public OpenTicketsModel(ITicketRepository ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }

        public List<OpenTicketViewModel> OpenTickets { get; set; }

        public void OnGet()
        {
            // select ile tablodan istenilen sütünlar çekilebilir.
            OpenTickets =  _ticketRepo.Where(x => x.CurrentState == (int)DomainEvents.Src.Domain.Tickets.Entities.TicketStates.Opened).Select(a=> new OpenTicketViewModel { 
           
               OpenDate = a.CurrentDate,
               Description = a.Description,
               Key = a.Id,
               Subject = a.Subject

           }).ToList();


        }
    }
}
