using Application.Repository;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository
{
    public class FAQCategoriesRepository : BaseRepository<FAQCategory>, IFAQCategoriesRepository

    {
        public FAQCategoriesRepository(ApplicationDbContext _context) : base(_context) { }

        public async Task<List<FAQCategory>> GetAll()
        {
            return await _context.FAQCategories.ToListAsync();
        }

        public bool IsContextNull()
        {
            return _context.FAQCategories == null;
        }
    }
}
