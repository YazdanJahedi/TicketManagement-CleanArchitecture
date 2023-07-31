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

        public async Task<Ticket?> FindByIdAsync(long id)
        {
            return await _context.Tickets
                .Include(t => t.Messages)
                .Include(m => m.Creator)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        // make Adds return Task too...
        public async Task RemoveAsync(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }

    }
}
