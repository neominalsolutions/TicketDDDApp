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
    public class TaskContext: DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;

        public TaskContext(DbContextOptions<TaskContext> options,
       IDomainEventDispatcher dispatcher)
       : base(options)
        {
            _dispatcher = dispatcher;
        }


        private async Task _dispatchDomainEvents()
        {
            var domainEventEntities = ChangeTracker.Entries<IEntity>()
                .Select(po => po.Entity)
                .Where(po => po.DomainEvents.Any())
                .ToArray();

            foreach (var entity in domainEventEntities)
            {
                foreach (var @event in entity.DomainEvents)
                {
                    await _dispatcher.Dispatch(@event);
                }
                   
            }
        }



        public override int SaveChanges()
        {
            _dispatchDomainEvents().GetAwaiter().GetResult();
            var res = base.SaveChanges();
            return res;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _dispatchDomainEvents();
            var res = await base.SaveChangesAsync(cancellationToken);
            return res;
        }

    }
}
