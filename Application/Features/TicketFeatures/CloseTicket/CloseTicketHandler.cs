using Application.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TicketFeatures.CloseTicket
{
    public class CloseTicketHandler : IRequestHandler<CloseTicketRequest>
    {
        private readonly ITicketsRepository _ticketRepository;


        public CloseTicketHandler(ITicketsRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Unit> Handle(CloseTicketRequest request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.FindByIdAsync(request.TicketId);
            if (ticket == null) throw new DirectoryNotFoundException("Ticket not found");

            if (ticket.Status == "Closed")
            {
                ticket.Status = "Checked";
                ticket.CloseDate = null;
            }
            else
            {
                ticket.Status = "Closed";
                ticket.CloseDate = DateTime.Now;
            }

            await _ticketRepository.UpdateAsync(ticket);
            return Unit.Value;
        }
    }
}
