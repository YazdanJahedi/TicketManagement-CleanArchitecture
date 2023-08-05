using Application.Features.FaqItemFeatures.GetAllFaqItems;
using Application.Repository;
using MediatR;


namespace Application.Features.FaqItemFeatures.Queries
{
    public class GetFaqItemsHandler : IRequestHandler<GetFaqItemsRequest, IEnumerable<GetFaqItemsResponse>>
    {
        private readonly IFAQItemsRepository _faqItemsRepository;

        public GetFaqItemsHandler(IFAQItemsRepository faqItemsRepository)
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
