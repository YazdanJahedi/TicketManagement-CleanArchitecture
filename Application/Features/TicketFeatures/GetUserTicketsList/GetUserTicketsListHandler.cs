using Application.Common.Exceptions;
using Application.Features.TicketFeatures.GetTicketsList;
using Application.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TicketFeatures.GetUserTicketsList
{
    public class GetUserTicketsListHandler : IRequestHandler<GetUserTicketsListRequest, IEnumerable<GetTicketsListResponse>?>
    {
        public readonly IUsersRepository _usersRepository;

        public GetUserTicketsListHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<IEnumerable<GetTicketsListResponse>?> Handle(GetUserTicketsListRequest request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.FindByNameAsync(request.UserName);
            if (user == null) throw new NotFoundException("user not found");

            var response = user.Tickets?.Select(t =>
                new GetTicketsListResponse
                {
                    Id = t.Id,
                    Title = t.Title,
                    Status = t.Status,
                    FirstResponseDate = t.FirstResponseDate,
                    CloseDate = t.CloseDate,
                }
            );

            return response;
        }
    }
}
