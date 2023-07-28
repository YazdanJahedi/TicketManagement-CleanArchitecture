using Application.Repository;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class FAQItemsRepository: BaseRepository<FAQItem> , IFAQItemsRepository
    {
        public FAQItemsRepository(ApplicationDbContext _context) : base(_context) { }

        public async Task<IEnumerable<FAQItem>> FindAllByCategoryIdAsync(long categoryId)
        {
            return await _context.FAQItems.Where(a => a.CategoryId == categoryId).ToListAsync();
        }

    }
}
