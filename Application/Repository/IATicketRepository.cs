using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Repository
{
    public interface IATicketRepository : IBaseRepository<Ticket>
    {
        ActionResult<Ticket> GetTickets(bool isCheckd);
        Task<IActionResult> DeleteTicket(long ticketId);
    }
}
