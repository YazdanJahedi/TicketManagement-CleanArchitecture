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

        public IEnumerable<FAQItem> FindAllByCategoryId(long categoryId)
        {
            return _context.FAQItems.Where(a => a.CategoryId == categoryId);
        }

        public override void CheckNull()
        {
            if(null == _context.FAQItems)
            {
                throw new Exception("context is null");
            }
        }

        public override void Add(FAQItem entity)
        {
            _context.FAQItems.Add(entity);
            _context.SaveChanges();
        }

    }
}
