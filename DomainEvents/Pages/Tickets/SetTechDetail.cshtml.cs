using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainEvents.Helpers.Enums;
using DomainEvents.Src.Domain.Tickets.Entities;
using DomainEvents.Src.Domain.Tickets.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomainEvents.Pages.Tickets
{
    // ViewModel sayfada gösterilecek ihtiyaç duyulan tüm modelleri içinde barýndýran nesne.
    public class TicketDetailVM
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public string OpenDate { get; set; }

        public string[] TicketPriorityLevels
        {
            get
            {
                return Enum.GetNames(typeof(TicketPriorityLevels));
            }
        }

        public string[] TicketDifficultyLevels
        {
            get
            {
                return Enum.GetNames(typeof(TicketDifficultyLevels));
            }
        }

    }

    public class SetTechDetailInputModel
    {
        public string LevelOfDifficultyValue { get; set; }
        public string LevelOfPriorityValue { get; set; }

        public string TicketId { get; set; }
    }

    public class SetTechDetailModel : PageModel
    {
        private readonly ITicketRepository ticketRepository;

        public SetTechDetailModel(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }




        public TicketDetailVM TicketVM { get; set; } = new TicketDetailVM();

        [BindProperty]
        public SetTechDetailInputModel Input { get; set; } = new SetTechDetailInputModel();

        public void OnGet(string id)
        {
            var ticket = ticketRepository.Find(id);
            Input.TicketId = id;

            TicketVM = new TicketDetailVM();
            TicketVM.Subject = ticket.Subject;
            TicketVM.Description = ticket.Description;
            TicketVM.OpenDate = ticket.CurrentDate.ToShortDateString();
        }


        public void OnPostSave()
        {
            var ticket = ticketRepository.Find(Input.TicketId);

            int levelOfDifficult = EnumHelper.GetEnumValue(
                Input.LevelOfDifficultyValue,
                typeof (TicketDifficultyLevels));

            int levelOfPriority = EnumHelper.GetEnumValue(
                Input.LevelOfPriorityValue, 
                typeof(TicketPriorityLevels));

            ticket.SetTechDetail((TicketPriorityLevels)levelOfPriority, (TicketDifficultyLevels)levelOfDifficult);

            TicketVM.Subject = ticket.Subject;
            TicketVM.Description = ticket.Description;
            TicketVM.OpenDate = ticket.CurrentDate.ToString();

            ticketRepository.Update(ticket);


        }
    }
}
