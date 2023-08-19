using Application.Common.Exceptions;
using Application.Dtos.MessageDtos;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using Domain.Entities;
using Domain.Enums;


namespace Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _userService;
        private readonly IMessageAttachmentService _messageAttachmentService;
        private readonly ITicketService _ticketService;

        public MessageService(IAuthService userService,
                              IUnitOfWork unitOfWork,
                              IMessageAttachmentService messageAttachmentService,
                              ITicketService ticketService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _messageAttachmentService = messageAttachmentService;
            _ticketService = ticketService;
        }

        public async Task Add(CreateMessageRequest request)
        {
            var claims = _userService.GetClaims();

            // check access permision to ticket
            var ticket = await _unitOfWork.TicketsRepository.GetByConditionAsync(t => t.Id == request.TicketId);
            if (ticket == null || (ticket.CreatorId != claims.Id && claims.Role == "User")) throw new NotFoundException("TicketId not found");
            if (ticket.Status == TicketStatus.Closed) throw new Exception("ticket is closed");

            var message = new Message
            {
                TicketId = request.TicketId,
                CreatorId = claims.Id,
                Text = request.Text,
                CreationDate = DateTime.Now,
            };

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    await _unitOfWork.MessagesRepository.AddAsync(message);
                    await _unitOfWork.SaveAsync();

                    if (request.Attacments != null)
                        await _messageAttachmentService.UploadRange(request.Attacments, message.Id);

                    _ticketService.UpdateAfterSendMessage(ticket, claims.Role);

                    await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                } 
                catch (Exception) 
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
