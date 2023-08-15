using Application.Dtos.FaqDtos;

namespace Application.Interfaces.Service
{
    public interface IFaqService
    {
        public Task<IEnumerable<GetFaqCategoriesResponse>> GetFaqCategories();
        public Task<IEnumerable<GetFaqItemsResponse>> GetFaqItems(long faqCategoryId);
    }
}
