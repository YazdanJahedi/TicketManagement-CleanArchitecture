using Application.Common.Exceptions;
using Application.Repository;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TicketFeatures.DeleteTicket
{

    internal class DeleteTicketHandler : IRequestHandler<DeleteTicketRequest>
    {
        private readonly ITicketsRepository _ticketsRepository;
        public DeleteTicketHandler(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
        }

        public async Task<Unit> Handle(DeleteTicketRequest request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketsRepository.FindByIdAsync(request.TicketId);

            if (ticket == null)
            {
                throw new NotFoundException("Ticket not found");
            }

            await _ticketsRepository.RemoveAsync(ticket);
            return Unit.Value;
        }
    }
}
