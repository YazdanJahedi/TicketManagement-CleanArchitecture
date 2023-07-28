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

        public IEnumerable<Ticket> FindAllByCreatorId(long creatorId)
        {
            return _context.Tickets.Where(a => a.CreatorId == creatorId);
        }

        public Ticket? FindById(long id)
        {
            return _context.Tickets.Find(id);
        }

        // make Adds return Task too...
        public async Task RemoveAsync(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }

    }
}
