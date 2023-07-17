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
    public class FAQCategoriesRepository : BaseRepository<FAQCategory>, IFAQCategoriesRepository

    {
        public FAQCategoriesRepository(ApplicationDbContext _context) : base(_context) { }

        public Task<List<FAQCategory>> GetAll()
        {
            return _context.FAQCategories.ToListAsync();
        }

        public bool IsContextNull()
        {
            return _context.FAQCategories == null;
        }
    }
}
