using Application.Common.Exceptions;
using Application.Repository;
using MediatR;
using System.Security.Claims;
using Domain.Entities;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.AspNetCore.Http;

namespace Application.Features.TicketFeatures.CreateTicket
{
    public class CreateTicketHandler : IRequestHandler<CreateTicketRequest>
    {

        private readonly ITicketsRepository _ticketRepository;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateTicketHandler(ITicketsRepository ticketRepository, IMessagesRepository messagesRepository, IHttpContextAccessor httpContextAccessor)
        {
            _ticketRepository = ticketRepository;
            _messagesRepository = messagesRepository;
            _httpContextAccessor = httpContextAccessor;
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

            // check validation
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
                Status = "Not Checked",
            };

            var firstMesage = new Message
            {
                TicketId = ticket.Id,
                CreatorId = userId,
                CreationDate = DateTime.Now,
                Text = request.Description,

            };

            await _ticketRepository.AddAsync(ticket);
            await _messagesRepository.AddAsync(firstMesage);

            return Unit.Value;

        }
    }
}
