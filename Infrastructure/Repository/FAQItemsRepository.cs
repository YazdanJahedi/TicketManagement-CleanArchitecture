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

        public IQueryable<FAQItem> FindAllByCategoryId(long id)
        {
            return _context.FAQItems.Where(a => a.CategoryId == id);
        }

        public bool IsContextNull()
        {
            return _context.FAQItems == null;
        }
    }
}
