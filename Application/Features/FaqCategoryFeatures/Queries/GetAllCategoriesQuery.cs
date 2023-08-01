using Application.DTOs.FaqCategoryDto;
using Application.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FaqCategoryFeatures.Queries
{

    public class GetAllCategoriesQuery : IRequestHandler<GetFaqCategoriesRequest, IEnumerable<GetFaqCategoriesResponse>>
    {
        private readonly IFAQCategoriesRepository _faqCategoriesRepository;

        public GetAllCategoriesQuery(IFAQCategoriesRepository faqCategoriesRepository)
        {
            _faqCategoriesRepository = faqCategoriesRepository;
        }

        public async Task<IEnumerable<GetFaqCategoriesResponse>> Handle(GetFaqCategoriesRequest request, CancellationToken cancellationToken)
        {

            var categories = await _faqCategoriesRepository.GetAllAsync();
            var response = categories.Select(c =>
                new GetFaqCategoriesResponse
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName,
                }
            );
            return response;
        }
    }

    
}

 
