using Application.Features.TicketFeatures.GetTicketsList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TicketFeatures.GetUserTicketsList
{
    public record GetUserTicketsListRequest : IRequest<IEnumerable<GetTicketsListResponse>>
    {
        public string UserName { get; set; }
        public GetUserTicketsListRequest(string username) 
        {
            UserName = username;
        }
    }
}
