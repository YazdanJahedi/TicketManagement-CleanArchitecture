using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TicketDtos
{
    public record PostCloseTicketRequest : IRequest
    {
        public long TicketId { get; set; }

        public PostCloseTicketRequest(long ticketId)
        {
            TicketId = ticketId;
        }
    }
}
