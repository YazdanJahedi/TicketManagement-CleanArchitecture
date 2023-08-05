using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FaqItemFeatures.GetAllFaqItems
{
    public record GetFaqItemsRequest : IRequest<IEnumerable<GetFaqItemsResponse>>
    {
        public long Id { get; set; }

        public GetFaqItemsRequest(long id)
        {
            Id = id;
        }
    }
}
