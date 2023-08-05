using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TicketFeatures.DeleteTicket
{
    public record DeleteTicketRequest : IRequest
    {
        public long TicketId { get; set; }

        public DeleteTicketRequest(long ticketid)
        {
            TicketId = ticketid;
        }
    }
}
