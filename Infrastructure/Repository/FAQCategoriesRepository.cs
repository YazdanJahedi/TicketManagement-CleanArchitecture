using Application.Repository;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class FAQCategoriesRepository : BaseRepository<FAQCategory>, IFAQCategoriesRepository

    {
        public FAQCategoriesRepository(ApplicationDbContext _context) : base(_context) { }

        public async Task<IEnumerable<FAQCategory>> GetAlld()
        {
            return await _context.FAQCategories.ToListAsync();
        }

        public override void CheckNull()
        {
            if (null == _context.FAQCategories)
            {
                throw new Exception("context is null");  // todo : make an exeption class
            }
        }

        public override void Add(FAQCategory entity)
        {
            _context.FAQCategories.Add(entity);
            _context.SaveChanges();
        }
    }
}
