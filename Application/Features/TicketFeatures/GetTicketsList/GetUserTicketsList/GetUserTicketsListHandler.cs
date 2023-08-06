using Application.Common.Exceptions;
using Application.Repository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TicketFeatures.GetTicketsList.GetUserTicketsList
{
    public class GetUserTicketsListHandler : IRequestHandler<GetUserTicketsListRequest, IEnumerable<GetTicketsListResponse>?>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public GetUserTicketsListHandler(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetTicketsListResponse>?> Handle(GetUserTicketsListRequest request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.FindByNameAsync(request.UserName);
            if (user == null) throw new NotFoundException("user not found");

            var response = _mapper.Map<IEnumerable<GetTicketsListResponse>>(user.Tickets);

            return response;
        }
    }
}
