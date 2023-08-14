using Application.Common.Exceptions;
using Application.Dtos.MessageDtos;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using Domain.Entities;
using Domain.Enums;


namespace Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IAuthService _userService;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IMessageAttachmentService _messageAttachmentService;
        private readonly ITicketsRepository _ticketsRepository;
        private readonly ITicketService _ticketService;

        public MessageService(IAuthService userService,
                              IMessagesRepository messagesRepository,
                              ITicketsRepository ticketsRepository,
                              IMessageAttachmentService messageAttachmentService,
                              ITicketService ticketService)
        {
            _userService = userService;
            _messagesRepository = messagesRepository;
            _ticketsRepository = ticketsRepository;
            _messageAttachmentService = messageAttachmentService;
            _ticketService = ticketService;
        }

        public async Task Add(CreateMessageRequest request)
        {
            var claims = _userService.GetClaims();

            // check access permision to ticket
            var ticket = await _ticketsRepository.GetByConditionAsync(t => t.Id == request.TicketId);
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
            if (request.Attacments != null)
               await _messageAttachmentService.UploadRange(request.Attacments, message.Id);
            
            await _ticketService.UpdateTicketAfterSendNewMessage(ticket, claims.Role);
        }
    }
}
