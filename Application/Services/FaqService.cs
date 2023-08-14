using Application.Dtos.FaqDtos;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using AutoMapper;

namespace Application.Services
{
    public class FaqService : IFaqService
    {
        private readonly IFAQCategoriesRepository _faqCategoriesRepository;
        private readonly IFAQItemsRepository _faqItemsRepository;
        private readonly IMapper _mapper;
        public FaqService(IFAQCategoriesRepository faqCategoriesRepository,
                          IFAQItemsRepository faqItemsRepository,
                          IMapper mapper)
        {
            _faqCategoriesRepository = faqCategoriesRepository;
            _faqItemsRepository = faqItemsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetFaqCategoriesResponse>> GetFaqCategories()
        {
            var categories = await _faqCategoriesRepository.GetAllAsync();

            var response = _mapper.Map<IEnumerable<GetFaqCategoriesResponse>>(categories);

            return response;
        }

        public async Task<IEnumerable<GetFaqItemsResponse>> GetFaqItems(long faqCategoryId)
        {
            var items = await _faqItemsRepository.GetAllAsync(condition: a => a.CategoryId == faqCategoryId);

            var response = _mapper.Map<IEnumerable<GetFaqItemsResponse>>(items);

            return response;
        }
    }
}
