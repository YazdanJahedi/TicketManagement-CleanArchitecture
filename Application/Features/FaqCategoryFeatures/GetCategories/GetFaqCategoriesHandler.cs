using Application.Features.FaqCategoryFeatures.GetAllCategories;
using Application.Repository;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FaqCategoryFeatures.Queries
{

    public class GetFaqCategoriesHandler : IRequestHandler<GetFaqCategoriesRequest, IEnumerable<GetFaqCategoriesResponse>>
    {
        private readonly IFAQCategoriesRepository _faqCategoriesRepository;
        private readonly IMapper _mapper;

        public GetFaqCategoriesHandler(IFAQCategoriesRepository faqCategoriesRepository, IMapper mapper)
        {
            _faqCategoriesRepository = faqCategoriesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetFaqCategoriesResponse>> Handle(GetFaqCategoriesRequest request, CancellationToken cancellationToken)
        {

            var categories = await _faqCategoriesRepository.GetAllAsync();
 
            var response = _mapper.Map<IEnumerable<GetFaqCategoriesResponse>>(categories);

            return response;
        }
    }


}


