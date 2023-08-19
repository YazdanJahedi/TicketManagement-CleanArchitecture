using Application.Dtos.FaqDtos;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using AutoMapper;

namespace Infrastructure.Services
{
    public class FaqService : IFaqService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FaqService(IUnitOfWork unitOfWork,
                          IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetFaqCategoriesResponse>> GetFaqCategories()
        {
            var categories = await _unitOfWork.FaqCategoriesRepository.GetAllAsync();

            var response = _mapper.Map<IEnumerable<GetFaqCategoriesResponse>>(categories);

            return response;
        }

        public async Task<IEnumerable<GetFaqItemsResponse>> GetFaqItems(long faqCategoryId)
        {
            var items = await _unitOfWork.FaqItemsRepository.GetAllAsync(condition: a => a.CategoryId == faqCategoryId);

            var response = _mapper.Map<IEnumerable<GetFaqItemsResponse>>(items);

            return response;
        }
    }
}
