using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TicketFeatures.CloseTicket
{
    public record CloseTicketRequest : IRequest
    {
        public long TicketId { get; set; }

        public CloseTicketRequest(long ticketId)
        {
            TicketId = ticketId;
        }
    }
}
