using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class OpenTicketInputModel
    {
        [Required(ErrorMessage ="Müþteri seç")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Konu gir")]
        [StringLength(20, ErrorMessage = "En fazla 20 karakter gir")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Açýklama gir")]
        [StringLength(500,ErrorMessage = "En fazla 500 karakter gir")]
        public string Description { get; set; }


    }

    public class OpenTicketModel : PageModel
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketAssignmentDomainService _ticketAssignmentDomainService;

        public OpenTicketModel(ITicketRepository ticketRepository, ITicketAssignmentDomainService ticketAssignmentDomainService)
        {
            _ticketRepository = ticketRepository;
            _ticketAssignmentDomainService = ticketAssignmentDomainService;
        }

        [BindProperty]
        public OpenTicketInputModel InputModel { get; set; }

        
        public IEnumerable<SelectListItem> Customers { 
            get {
                return SelectHelper.GetItems("Rabia", "Ayþe", "Aslý", "Berkay");
            } } 

        public void OnGet()
        {


            // 1. test case


            //var t = new Ticket("S2411", "D23411", "12342");
            //t.OpenTicket("mert.alptekin@neominal.com", "mert");

            //_ticketRepository.Add(t);

            // var tickets = _ticketRepository.List();

            //var ticket = _ticketRepository.Find("1d4754ed-e44a-462d-9085-2ec51e5b9101");

            // 2. test case

            //ticket.SetTechDetail(TicketPriorityLevels.Critical, TicketDifficultyLevels.Medium);
            //_ticketRepository.Update(ticket);

            //3.test case
            //ticket.Assign("3", _ticketAssignmentDomainService);
            //_ticketRepository.Update(ticket);

        }

        public void OnPostOpenTicket()
        {
            if (ModelState.IsValid)
            {
                var t = new Ticket(InputModel.Subject,InputModel.Description, InputModel.CustomerName);
                t.OpenTicket("mert.alptekin@neominal.com", InputModel.CustomerName);

                _ticketRepository.Add(t);
                ViewData["Message"] = "Kayýt Baþarýlýdýr";
            }

           
        }
    }
}
