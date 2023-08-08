using Application.Repository;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class MessagesRepository : BaseRepository<Message>, IMessagesRepository
    {
        public MessagesRepository(ApplicationDbContext _context): base(_context) { }

        public async Task<IEnumerable<Message>> FindAllByTicketIdAsync(long ticketId)
        {
            return await _context.Messages.Where(a => a.TicketId == ticketId).ToListAsync();
        }

    }
}
