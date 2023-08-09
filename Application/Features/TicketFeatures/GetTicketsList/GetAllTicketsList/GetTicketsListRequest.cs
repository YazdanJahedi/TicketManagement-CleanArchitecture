using MediatR;
using Microsoft.AspNetCore.Server.HttpSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TicketFeatures.GetTicketsList.GetAllTicketsList
{
    public record GetTicketsListRequest : IRequest<IEnumerable<GetTicketsListResponse>> 
    {
        public int NumberOfReturningTickets { get; set; }

        public GetTicketsListRequest(int numberOfReturningTickets) 
        {
            NumberOfReturningTickets = numberOfReturningTickets;
        }

    }
}
