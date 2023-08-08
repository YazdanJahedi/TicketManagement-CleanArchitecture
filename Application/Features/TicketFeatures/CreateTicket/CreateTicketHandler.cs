using Application.Common.Exceptions;
using Application.Repository;
using MediatR;
using System.Security.Claims;
using Domain.Entities;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.AspNetCore.Http;
using Application.Interfaces;
using Domain.Enums;

namespace Application.Features.TicketFeatures.CreateTicket
{
    public class CreateTicketHandler : IRequestHandler<CreateTicketRequest>
    {

        private readonly ITicketsRepository _ticketRepository;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMessageAttachmentService _messageAttachmentService;

        public CreateTicketHandler(ITicketsRepository ticketRepository, IMessagesRepository messagesRepository,
                                   IHttpContextAccessor httpContextAccessor, IMessageAttachmentService messageAttachmentService)
        {
            _ticketRepository = ticketRepository;
            _messagesRepository = messagesRepository;
            _httpContextAccessor = httpContextAccessor;
            _messageAttachmentService = messageAttachmentService;
        }

        public IHttpContextAccessor Get_httpContextAccessor()
        {
            return _httpContextAccessor;
        }

        public async Task<Unit> Handle(CreateTicketRequest request, CancellationToken cancellationToken)
        {
            var idString = _httpContextAccessor.HttpContext?.User.
                Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var userId = Convert.ToInt64(idString);

            if (!CreateTicketValidator.IsValid(request))
            {
                throw new ValidationErrorException("Fields can not be empty");
            }

            // create a new ticket instance
            var ticket = new Ticket
            {
                CreatorId = userId,
                Title = request.Title,
                CreationDate = DateTime.Now,
                FaqCategoryId = request.FaqCatgoryId,
                Status = TicketStatus.NotChecked,
            };

            await _ticketRepository.AddAsync(ticket);

            var firstMesage = new Message
            {
                TicketId = ticket.Id,
                CreatorId = userId,
                CreationDate = DateTime.Now,
                Text = request.Description,

            };

            await _messagesRepository.AddAsync(firstMesage);
            if (request.Attachments != null)
                await _messageAttachmentService.SaveMultipeAttachments(request.Attachments, firstMesage.Id);

            return Unit.Value;

        }
    }
}
