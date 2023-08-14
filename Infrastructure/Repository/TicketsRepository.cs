using Application.Interfaces.Repository;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class TicketsRepository : BaseRepository<Ticket>, ITicketsRepository
    {
        public TicketsRepository(ApplicationDbContext _context): base(_context) {}

        public async Task<Ticket?> FindByIdAsync(long id)
        {
            return await _context.Tickets
                        .Include(t => t.Messages)!
                            .ThenInclude(m => m.Creator)
                        .Include(t => t.Messages)!
                            .ThenInclude(m => m.Attachments)
                        .FirstOrDefaultAsync(t => t.Id == id);

        }
        
        public async Task RemoveAsync(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _context.Entry(ticket).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
