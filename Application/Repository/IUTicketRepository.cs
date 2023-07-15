using Application.Features.CreateTicket;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Repository
{
    public interface IUTicketRepository : IBaseRepository<Ticket>
    {
        Task<ActionResult<Ticket>> PostTicket(CreateTicketRequest req);
        ActionResult<Ticket> GetTickes();
    }
}
