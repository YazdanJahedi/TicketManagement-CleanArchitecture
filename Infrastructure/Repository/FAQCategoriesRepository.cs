﻿using Application.Repository;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class FAQCategoriesRepository : BaseRepository<FAQCategory>, IFAQCategoriesRepository
    {
        public FAQCategoriesRepository(ApplicationDbContext _context) : base(_context) { }


        public async Task<IEnumerable<FAQCategory>> TestMethod()
        {
            return await _context.FAQCategories
                .Include(x => x.Items)
                .ToListAsync();
        }

    }
}
