using Application.Features.CreateTicket;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Repository
{
    public interface ITicketRepository : IBaseRepository<Ticket>
    {
        Task<ActionResult<Ticket>> PostTicket(CreateTicketRequest req);
        ActionResult<Ticket> GetTickes();
        ActionResult<Ticket> GetTickets(bool isCheckd);
        Task<IActionResult> DeleteTicket(long ticketId);
    }
}
