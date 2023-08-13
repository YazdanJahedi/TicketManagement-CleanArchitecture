using Application.Dtos.FaqDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IFaqService
    {
        public Task<IEnumerable<GetFaqCategoriesResponse>> GetFaqCategories();
        public Task<IEnumerable<GetFaqItemsResponse>> GetFaqItems(long faqCategoryId);
    }
}
