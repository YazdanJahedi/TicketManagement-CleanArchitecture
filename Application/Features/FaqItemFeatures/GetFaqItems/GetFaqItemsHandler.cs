using Application.Features.FaqItemFeatures.GetAllFaqItems;
using Application.Repository;
using AutoMapper;
using MediatR;


namespace Application.Features.FaqItemFeatures.Queries
{
    public class GetFaqItemsHandler : IRequestHandler<GetFaqItemsRequest, IEnumerable<GetFaqItemsResponse>>
    {
        private readonly IFAQItemsRepository _faqItemsRepository;
        private readonly IMapper _mapper;
        public GetFaqItemsHandler(IFAQItemsRepository faqItemsRepository, IMapper mapper)
        {
            _faqItemsRepository = faqItemsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetFaqItemsResponse>> Handle(GetFaqItemsRequest request, CancellationToken cancellationToken)
        {
            var items = await _faqItemsRepository.FindAllByCategoryIdAsync(request.Id);

            var response = _mapper.Map<IEnumerable<GetFaqItemsResponse>>(items);

            return response;
        }
    }
}
