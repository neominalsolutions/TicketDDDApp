using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainEvents.Helpers.Selects;
using DomainEvents.Src.Domain.Tickets.Entities;
using DomainEvents.Src.Domain.Tickets.Repositories;
using DomainEvents.Src.Domain.Tickets.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DomainEvents.Pages.Tickets
{

    public class ReadyForAssigmentViewModel
    {
        public string Subject { get; set; }
        public string Description { get; set; }

        public string LevelOfDifficulty { get; set; }

        public string LevelOfPriority { get; set; }

        public string TicketNumber { get; set; }



    }

    public class AssignmentInputModel
    {
        public string TicketId { get; set; }
        public string EmployeeId { get; set; }

    }


   

    public class ReadyForAssigmentTicketsModel : PageModel
    {
        private readonly ITicketRepository ticketRepository;
        private readonly ITicketAssignmentDomainService dService;

        public List<ReadyForAssigmentViewModel> ReadyForAssignments { get; set; }

        [BindProperty]
        public List<AssignmentInputModel> Inputs { get; set; } = new List<AssignmentInputModel>();

        public IEnumerable<SelectListItem> Employees
        {
            get
            {
                return SelectHelper.GetItems(
                    new KeyValuePair<string, int>("Ali", 1),
                    new KeyValuePair<string, int>("Ahmet", 2),
                    new KeyValuePair<string, int>("Hamza", 3),
                    new KeyValuePair<string, int>("Elif", 4));
            }
        }

     

        public ReadyForAssigmentTicketsModel(ITicketRepository ticketRepository, ITicketAssignmentDomainService dService)
        {
            this.ticketRepository = ticketRepository;
            this.dService = dService;
        }

        private void LoadReadyForAssignments()
        {
            ReadyForAssignments = ticketRepository.Where(x => x.CurrentState == (int)TicketStates.ReadyForAssignment).Select(a => new ReadyForAssigmentViewModel
            {
                Description = a.Description,
                Subject = a.Subject,
                LevelOfDifficulty = a.LevelOfDifficulty.ToString(),
                LevelOfPriority = a.LevelOfPriority.ToString(),
                TicketNumber = a.Id
            }).ToList();

            foreach (var item in ReadyForAssignments)
            {
                Inputs.Add(default(AssignmentInputModel));
            }
        }

        public void OnGet()
        {
            LoadReadyForAssignments();
        }


        public void OnPostAssign()
        {

            foreach (var item in Inputs)
            {

                // seçim yapýlmýssa
                if(item.EmployeeId != "-1")
                {
                    var ticket = ticketRepository.Find(item.TicketId);
                    ticket.Assign(item.EmployeeId, dService);
                    ticketRepository.Update(ticket);
                }
            }

            LoadReadyForAssignments();
         

        }
    }
}
