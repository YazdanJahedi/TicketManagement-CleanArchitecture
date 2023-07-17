using Application.Features.CreateResponse;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Repository
{
    public interface IResponseRepository : IBaseRepository<Response>
    {
        Task<ActionResult<Ticket>> PostResponse(long ticketId, CreateResponseRequest req);
        ActionResult<Ticket> GetResponses(long ticketId);
        

        // new
        ActionResult<Ticket> GetAllResponses();


    }
}
