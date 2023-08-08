using Application.Common.Exceptions;
using Application.Repository;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public GetTicketHandler(ITicketsRepository ticketsRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _ticketsRepository = ticketsRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
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

            var response = _mapper.Map<GetTicketResponse>(ticket);
            return response;
        }
    }
}
