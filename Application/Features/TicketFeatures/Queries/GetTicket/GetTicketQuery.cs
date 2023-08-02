using Application.Common.Exceptions;
using Application.DTOs.MessageDtos;
using Application.DTOs.TicketDtos;
using Application.DTOs.UserDtos;
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
            
            var ticket = await _ticketsRepository.FindByIdAsync(request.TicketId);
            if (ticket == null || ticket.Creator!.Id != userId) throw new NotFoundException("Ticket not found");
            
            var response = new GetTicketResponse
            {
                Title = ticket.Title,
                Messages = ticket.Messages!.Select(m =>
                    new GetMessageResponse
                    {
                        Text = m.Text,
                        Creator = new GetUserInformationRequest
                        {
                            Name = m.Creator!.Name , 
                            PhoneNumber = m.Creator.PhoneNumber ,
                            Email = m.Creator.Email ,
                            Role = m.Creator.Role ,
                        },
                        CreationDate = m.CreationDate,
                    }),
                FaqCategoryId = ticket.FaqCategoryId,
                Status = ticket.Status,
            };

            
            return response;
        }
    }
}
