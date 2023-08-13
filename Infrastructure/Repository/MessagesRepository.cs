using Application.Repository;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repository
{
    public class MessagesRepository : BaseRepository<Message>, IMessagesRepository
    {
        public MessagesRepository(ApplicationDbContext _context): base(_context) { }

    }
}
