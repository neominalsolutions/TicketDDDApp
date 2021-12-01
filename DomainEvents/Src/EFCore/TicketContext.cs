using DomainEvents.Src.Domain.Tickets.Entities;
using DomainEvents.Src.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DomainEvents.Src.EFCore
{
    public class TicketContext: DbContext
    {
        // IDomainEventDispatcher üzerinde save işlemi sırasında event fırlatmamız gereken bir şey varsa bunların sevk edilmesini bu servis ile çalıştırıcaz
        private readonly IDomainEventDispatcher _dispatcher;

        public TicketContext(DbContextOptions<TicketContext> options,
       IDomainEventDispatcher dispatcher)
       : base(options)
        {
            _dispatcher = dispatcher;
        }


        public DbSet<Ticket> Tickets { get; set; }


        private void _dispatchDomainEvents()
        {
            // bu uygulamada IEntity interfacesinde Implemente olmuş Entityleri bul ve bu entitylerin içerisinde AddEvents methodu ile tanımlanmış ne kadar DomainEvents var mı kontrolü yap. bulduklarını listeye at
            var domainEventEntities = ChangeTracker.Entries<IEntity>()
                .Select(po => po.Entity)
                .Where(po => po.DomainEvents.Any())
                .ToArray();

            foreach (var entity in domainEventEntities)
            {
                foreach (var @event in entity.DomainEvents)
                {
                    // entity atanmış eklenmiş ne kadar event varsa dipath ile bu eventlerin handlera gönderilmesi sağladığımız kod.
                     _dispatcher.Dispatch(@event);
                }
                   
            }
        }


        // Bir entity üzerinde bir değişiklik olduğunda bir olay gerçekleştiğinde bu entity db kayıt etmeden önce bu entity tanımlanmış eventlerin handlera gönderilmesini sağla.
        public override int SaveChanges()
        {
            _dispatchDomainEvents();
            // aray gir bu methodu çalıştır.
            var res = base.SaveChanges();
            return res;
        }

   

    }
}
