using Application.Features.FaqCategoryFeatures.GetAllCategories;
using Application.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FaqCategoryFeatures.Queries
{

    public class GetAllFaqCategoriesHandler : IRequestHandler<GetAllFaqCategoriesRequest, IEnumerable<GetAllFaqCategoriesResponse>>
    {
        private readonly IFAQCategoriesRepository _faqCategoriesRepository;

        public GetAllFaqCategoriesHandler(IFAQCategoriesRepository faqCategoriesRepository)
        {
            _faqCategoriesRepository = faqCategoriesRepository;
        }

        public async Task<IEnumerable<GetAllFaqCategoriesResponse>> Handle(GetAllFaqCategoriesRequest request, CancellationToken cancellationToken)
        {

            var categories = await _faqCategoriesRepository.GetAllAsync();
            var response = categories.Select(c =>
                new GetAllFaqCategoriesResponse
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName,
                }
            );
            return response;
        }
    }


}


