using Application.DTOs.FaqItemsDto;
using Application.Repository;
using MediatR;


namespace Application.Features.FaqItemFeatures.Queries
{
    public class GetAllFaqItemsQuery : IRequestHandler<GetFaqItemsRequest, IEnumerable<GetFaqItemsResponse>>
    {
        private readonly IFAQItemsRepository _faqItemsRepository;

        public GetAllFaqItemsQuery(IFAQItemsRepository faqItemsRepository)
        {
            _faqItemsRepository = faqItemsRepository;
        }
        public async Task<IEnumerable<GetFaqItemsResponse>> Handle(GetFaqItemsRequest request, CancellationToken cancellationToken)
        {
            var items = await _faqItemsRepository.FindAllByCategoryIdAsync(request.Id);
            var response = items.Select(i =>
                new GetFaqItemsResponse
                {
                    CategoryId = i.CategoryId,
                    Title = i.Title,
                    Description = i.Description,
                }
            );

            return response;
        }
    }
}
