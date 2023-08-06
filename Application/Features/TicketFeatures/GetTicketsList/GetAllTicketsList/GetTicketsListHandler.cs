using Application.Repository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TicketFeatures.GetTicketsList.GetAllTicketsList
{
    public class GetTicketsListHandler : IRequestHandler<GetTicketsListRequest, IEnumerable<GetTicketsListResponse>>
    {
        private readonly ITicketsRepository _ticketsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetTicketsListHandler(ITicketsRepository ticketsRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _ticketsRepository = ticketsRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetTicketsListResponse>> Handle(GetTicketsListRequest request, CancellationToken cancellationToken)
        {
            var idString = _httpContextAccessor.HttpContext?.User.
                Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var userId = Convert.ToInt64(idString);
            var role = _httpContextAccessor.HttpContext?.User.
                Claims.First(x => x.Type == ClaimTypes.Role).Value;

            IEnumerable<Ticket> tickets;
            if (role == "Admin")
                tickets = await _ticketsRepository.GetAllAsync();
            else // role == "User"
                tickets = await _ticketsRepository.FindAllByCreatorIdAsync(userId);

            var response = _mapper.Map<IEnumerable<GetTicketsListResponse>>(tickets);

            return response;
        }
    }
}
