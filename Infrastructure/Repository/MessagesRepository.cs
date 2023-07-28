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

        // can be changed?? ... Think...
        public Message? FindLastMessageByTicketId(long ticketId)
        {
            return  _context.Messages.OrderBy(m => m.CreationDate).LastOrDefault(e => e.TicketId == ticketId);
        }

        // delete this
        public void RemoveAllByTicketId(long ticketId)
        {
            var items = _context.Messages.Where(i => i.TicketId == ticketId);
            _context.Messages.RemoveRange(items);
            _context.SaveChanges();
        }

    }
}
