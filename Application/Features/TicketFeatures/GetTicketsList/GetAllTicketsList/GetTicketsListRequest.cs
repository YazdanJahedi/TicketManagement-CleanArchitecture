using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TicketFeatures.GetTicketsList.GetAllTicketsList
{
    public record GetTicketsListRequest : IRequest<IEnumerable<GetTicketsListResponse>>
    {
    }
}
