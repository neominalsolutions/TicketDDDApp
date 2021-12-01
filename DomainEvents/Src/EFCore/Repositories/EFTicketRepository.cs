using DomainEvents.Src.Domain.Tickets.Entities;
using DomainEvents.Src.Domain.Tickets.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DomainEvents.Src.EFCore.Repositories
{
    public class EFTicketRepository : ITicketRepository
    {
        private readonly TicketContext _context;

        public EFTicketRepository(TicketContext ticketContext)
        {
            _context = ticketContext;
        }

        public void Add(Ticket entity)
        {
            _context.Tickets.Add(entity);
            _context.SaveChanges();
        }

        public bool Any(Expression<Func<Ticket, bool>> expression)
        {
            return _context.Tickets.Any(expression);
        }

        public void Delete(string Id)
        {
            var entity = Find(Id);
            _context.Tickets.Remove(entity);
            _context.SaveChanges();

        }

        public Ticket Find(string Id)
        {
            return _context.Tickets.Include(x=> x.States).FirstOrDefault(x=> x.Id == Id);
        }

        public List<Ticket> List()
        {
            return _context.Tickets.Include(x=> x.States).ToList();
        }

        public void Update(Ticket entity)
        {
            _context.Tickets.Update(entity);
            _context.SaveChanges();
        }

        public IQueryable<Ticket> Where(Expression<Func<Ticket, bool>> expression)
        {
           //var query =  _context.Tickets.AsQueryable();

           //var query2 = query.Where(x => x.Id == "1");

           // var query3 = query2.Where(x => x.LevelOfPriority == (int)TicketDifficultyLevels.Easy);

           // var list = query3.ToList();
           // var lst2 = query2.Count();
           // var lst3 = query.Sum(x => x.LevelOfDifficulty);

            // AsQueryable() methodu ile veritabanından çelikecek olan değerleri query dönüştürürür.
            return _context.Tickets.Include(x=> x.States).Where(expression);
        }
    }
}
