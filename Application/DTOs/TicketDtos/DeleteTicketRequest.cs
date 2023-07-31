using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TicketDtos
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
