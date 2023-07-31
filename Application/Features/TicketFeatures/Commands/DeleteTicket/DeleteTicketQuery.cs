using Application.Common.Exceptions;
using Application.DTOs.TicketDtos;
using Application.Repository;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TicketFeatures.Commands.DeleteTicket
{

    internal class DeleteTicketQuery : IRequestHandler<DeleteTicketRequest>
    {
        private readonly ITicketsRepository _ticketsRepository;
        public DeleteTicketQuery(ITicketsRepository ticketsRepository) 
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
