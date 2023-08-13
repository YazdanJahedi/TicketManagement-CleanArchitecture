using Application.Interfaces.Repository;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repository
{
    public class FAQItemsRepository: BaseRepository<FAQItem> , IFAQItemsRepository
    {
        public FAQItemsRepository(ApplicationDbContext _context) : base(_context) { }

    }
}
