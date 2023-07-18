using Application.Repository;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repository
{
    public class MessagesRepository : BaseRepository<Message>, IMessagesRepository
    {
        public MessagesRepository(ApplicationDbContext _context): base(_context) { }
/*
        public void AddResponseAsync(Response response)
        {
            _context.Responses.Add(response);
        }

        public IQueryable<Response> FindAllResonsesByTicketId(long ticketId)
        {
            return _context.Responses.Where(a => a.TicketId == ticketId);
        }

        public bool IsContextNull()
        {
            return _context.Responses == null;
        }

        public async void RemoveListOfResponses(List<Response> items)
        {
            foreach (var item in items)
            {
                _context.Responses.Remove(item);
            }
            await _context.SaveChangesAsync();
        }*/
    }
}
