using Application.Common.Exceptions;
using Application.Interfaces.Service;
using Application.Repository;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MessageFeatures.CreateMessage
{
    public class CreateMessageHandler : IRequestHandler<CreateMessageRequest>
    {
        private readonly ITicketsRepository _ticketsRepository;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMessageAttachmentService _messageAttachmentService;
        private readonly ITicketService _ticketService;

        public CreateMessageHandler(ITicketsRepository ticketsRepository, IMessagesRepository messagesRepository,
                                    IHttpContextAccessor httpContextAccessor, IMessageAttachmentService messageAttachmentService
                                    , ITicketService ticketService)
        {
            _httpContextAccessor = httpContextAccessor;
            _ticketsRepository = ticketsRepository;
            _messagesRepository = messagesRepository;
            _messageAttachmentService = messageAttachmentService;
            _ticketService = ticketService;
        }

        public async Task<Unit> Handle(CreateMessageRequest request, CancellationToken cancellationToken)
        {
            var idString = _httpContextAccessor.HttpContext?.User.
                Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var userId = Convert.ToInt64(idString);
            var role = _httpContextAccessor.HttpContext?.User.
                Claims.First(x => x.Type == ClaimTypes.Role).Value;
            if (role == null) throw new NotFoundException("user not found");

            // check access permision to ticket
            var ticket = await _ticketsRepository.FindByIdAsync(request.TicketId, false);
            if (ticket == null || (ticket.CreatorId != userId && role == "User")) throw new NotFoundException("TicketId not found");
            if (ticket.Status == TicketStatus.Closed) throw new Exception("ticket is closed");

            var message = new Message
            {
                TicketId = request.TicketId,
                CreatorId = userId,
                Text = request.Text,
                CreationDate = DateTime.Now,
            };

            await _messagesRepository.AddAsync(message);
            if (request.Attacments != null) 
                await _messageAttachmentService.SaveMultipeAttachments(request.Attacments, message.Id);

            await _ticketService.UpdateTicketAfterSendNewMessage(ticket, role);

            return Unit.Value;
        }
    }
}
