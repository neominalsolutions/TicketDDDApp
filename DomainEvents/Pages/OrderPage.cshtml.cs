using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainEvents.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomainEvents.Pages
{
    public class OrderPageModel : PageModel
    {
        private TaskContext _orderingContext;

        public OrderPageModel(TaskContext orderingContext)
        {
            _orderingContext = orderingContext;
        }
        public void OnGet()
        {
        }

        public void OnPost()
        {

            var order = new Order();
            order.DoOrder("21321321", "32423432", "Mert Alptekin", DateTime.Now.AddDays(30));

            _orderingContext.Add(order);
            _orderingContext.SaveChanges();

        }
    }
}
