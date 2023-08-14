using Application.Common.Exceptions;
using Application.Dtos.MessageDtos;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IAuthService _userService;
        private readonly IMessagesRepository _messagesRepository;
        private readonly ITicketsRepository _ticketsRepository;

        public MessageService(IAuthService userService, IMessagesRepository messagesRepository, ITicketsRepository ticketsRepository)
        {
            _userService = userService;
            _messagesRepository = messagesRepository;
            _ticketsRepository = ticketsRepository;
        }

        public async Task AddMessage(CreateMessageRequest request)
        {
            var claims = _userService.GetClaims();

            // check access permision to ticket
            var ticket = await _ticketsRepository.FindByIdAsync(request.TicketId, false);
            if (ticket == null || (ticket.CreatorId != claims.Id && claims.Role == "User")) throw new NotFoundException("TicketId not found");
            if (ticket.Status == TicketStatus.Closed) throw new Exception("ticket is closed");

            var message = new Message
            {
                TicketId = request.TicketId,
                CreatorId = claims.Id,
                Text = request.Text,
                CreationDate = DateTime.Now,
            };

            await _messagesRepository.AddAsync(message);
            //if (request.Attacments != null)
            //   await _messageAttachmentService.SaveMultipeAttachments(request.Attacments, message.Id);
            //await _ticketService.UpdateTicketAfterSendNewMessage(ticket, role);
        }
    }
}
