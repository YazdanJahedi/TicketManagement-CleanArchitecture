using Application.Common.Exceptions;
using Application.DTOs.MessageDtos;
using Application.DTOs.TicketDtos;
using Application.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace Application.Features.TicketFeatures.Queries.GetTicket
{
    public class GetTicketQuery : IRequestHandler<GetTicketRequest, GetTicketResponse>
    {
        private readonly ITicketsRepository _ticketsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetTicketQuery(ITicketsRepository ticketsRepository, IHttpContextAccessor httpContextAccessor)
        {
            _ticketsRepository = ticketsRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetTicketResponse> Handle(GetTicketRequest request, CancellationToken cancellationToken)
        {
            var idString = _httpContextAccessor.HttpContext?.User
                .Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var userId = Convert.ToInt64(idString);

            // check if ticket is owned by uers or not .... todo...


            var ticket = await _ticketsRepository.FindByIdAsync(request.TicketId);
            if (ticket == null) throw new NotFoundException("Ticket not found");

            var response = new GetTicketResponse
            {
                Title = ticket.Title,
                Messages = ticket.Messages?.Select(m =>
                    new GetMessageResponse
                    {
                        Text = m.Text,
                        Creator = m.Creator,
                        CreationDate = m.CreationDate,
                    }),
                FaqCategoryId = ticket.FaqCategoryId,
            };

            return response;
        }
    }
}
