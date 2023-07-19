using Application.Repository;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class TicketsRepository : BaseRepository<Ticket>, ITicketsRepository
    {
        public TicketsRepository(ApplicationDbContext _context): base(_context) {}

        public override void Add(Ticket entity)
        {
            _context.Tickets.Add(entity);
            _context.SaveChanges(); 
        }

        public override void CheckNull()
        {
            if (null == _context.Tickets) throw new Exception("context is null");
        }

        public override IEnumerable<Ticket> GetAll()
        {
            return _context.Tickets.ToList();
        }

        public IEnumerable<Ticket> FindAllByCreatorId(long creatorId)
        {
            return _context.Tickets.Where(a => a.CreatorId == creatorId);
        }

        public Ticket? FindById(long id)
        {
            return _context.Tickets.Find(id);
        }

        public void Remove(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
        }

    }
}
