using Application.Repository;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repository
{
    public class MessagesRepository : BaseRepository<Message>, IMessagesRepository
    {
        public MessagesRepository(ApplicationDbContext _context): base(_context) { }

        public IEnumerable<Message> FindAllByTicketId(long ticketId)
        {
            return _context.Messages.Where(a => a.TicketId == ticketId);
        }

        public Message? FindLastMessageByTicketId(long ticketId)
        {
            return  _context.Messages.LastOrDefault(e => e.TicketId == ticketId);
        }

        public void RemoveAllByTicketId(long ticketId)
        {
            var items = _context.Messages.Where(i => i.TicketId == ticketId);
            _context.Messages.RemoveRange(items);
            _context.SaveChanges();
        }

        public override void CheckNull()
        {
            if (null == _context.Messages) throw new Exception("context is null");
        }

        public override void Add(Message entity)
        {
            _context.Messages.Add(entity);
            _context.SaveChanges();
        }

    }
}
