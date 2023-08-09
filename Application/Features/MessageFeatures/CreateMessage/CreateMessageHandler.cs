using Application.Common.Exceptions;
using Application.Interfaces;
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

        public CreateMessageHandler(ITicketsRepository ticketsRepository, IMessagesRepository messagesRepository,
                                    IHttpContextAccessor httpContextAccessor, IMessageAttachmentService messageAttachmentService)
        {
            _httpContextAccessor = httpContextAccessor;
            _ticketsRepository = ticketsRepository;
            _messagesRepository = messagesRepository;
            _messageAttachmentService = messageAttachmentService;
        }

        public async Task<Unit> Handle(CreateMessageRequest request, CancellationToken cancellationToken)
        {
            var idString = _httpContextAccessor.HttpContext?.User.
                Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var userId = Convert.ToInt64(idString);
            var role = _httpContextAccessor.HttpContext?.User.
                Claims.First(x => x.Type == ClaimTypes.Role).Value;

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

            // fill status field and first-response-date
            bool ticketNeedUpdate = false;
            if (role == "User" && ticket.Status != TicketStatus.NotChecked)
            {
                ticket.Status = TicketStatus.NotChecked;
                ticketNeedUpdate = true;
            }
            else if (role == "Admin" && ticket.Status != TicketStatus.Checked) // enum
            {
                if (ticket.FirstResponseDate == null) ticket.FirstResponseDate = DateTime.Now;
                ticket.Status = TicketStatus.Checked;
                ticketNeedUpdate = true;
            }
            if (ticketNeedUpdate) await _ticketsRepository.UpdateAsync(ticket);

            return Unit.Value;
        }
    }
}
