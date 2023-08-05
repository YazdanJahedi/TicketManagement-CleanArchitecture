using Application.Common.Exceptions;
using Application.DTOs;
using Application.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace Application.Features.TicketFeatures.GetTicket
{
    public class GetTicketHandler : IRequestHandler<GetTicketRequest, GetTicketResponse>
    {
        private readonly ITicketsRepository _ticketsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetTicketHandler(ITicketsRepository ticketsRepository, IHttpContextAccessor httpContextAccessor)
        {
            _ticketsRepository = ticketsRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetTicketResponse> Handle(GetTicketRequest request, CancellationToken cancellationToken)
        {
            var idString = _httpContextAccessor.HttpContext?.User
                .Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var userId = Convert.ToInt64(idString);
            var role = _httpContextAccessor.HttpContext?.User
                .Claims.First(x => x.Type == ClaimTypes.Role).Value;

            var ticket = await _ticketsRepository.FindByIdAsync(request.TicketId);
            if (ticket == null || role == "User" && ticket.Creator!.Id != userId) throw new NotFoundException("Ticket not found");

            var response = new GetTicketResponse
            {
                Title = ticket.Title,
                Messages = ticket.Messages!.Select(m =>
                    new GetMessageDetailsDto
                    {
                        Text = m.Text,
                        Creator = new GetUserInformationDto
                        {
                            Name = m.Creator!.Name,
                            PhoneNumber = m.Creator.PhoneNumber,
                            Email = m.Creator.Email,
                            Role = m.Creator.Role,
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
