using Application.Common.Exceptions;
using Application.DTOs.MessageDtos;
using Application.Repository;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MessageFeatures.Commands.CreateMessage
{
    public class CreateMessageCommand : IRequestHandler<CreateMessageRequest>
    {
        private readonly ITicketsRepository _ticketsRepository;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateMessageCommand(ITicketsRepository ticketsRepository, IMessagesRepository messagesRepository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _ticketsRepository = ticketsRepository;
            _messagesRepository = messagesRepository;
        }

        public async Task<Unit> Handle(CreateMessageRequest request, CancellationToken cancellationToken)
        {
            var idString = _httpContextAccessor.HttpContext?.User.
                Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var userId = Convert.ToInt64(idString);

            // check validation
            if (!CreateMessageValidator.IsValid(request))
            {
                throw new ValidationErrorException("Title can not be empty");
            }

            // check access validation to ticket
            var ticket = await _ticketsRepository.FindByIdAsync(request.TicketId);
            if (ticket == null || ticket.CreatorId != userId) throw new NotFoundException("TicketId not found");
            
            var response = new Message
            {
                TicketId = request.TicketId,
                CreatorId = userId,
                Text = request.Text,
                CreationDate = DateTime.Now,
            };

            await _messagesRepository.AddAsync(response);

            return Unit.Value;
        }
    }
}
