using Application.Features.CreateResponse;
using Application.Repository;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase                              
    {

        private readonly ITicketRepository _ticketRepository;

        public AdminController(ApplicationDbContext context, ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        [HttpGet("tickets")]
        public ActionResult<Ticket> GetTickets(bool isCheckd = false)
        {
            if (_ticketRepository.IsContextNull())
            {
                return NotFound();
            }

            var items = _ticketRepository.FindAllByIsChecked(isCheckd).ToList();

            return Ok(items);
        }

        [HttpDelete("{ticketId}")]
        public async Task<IActionResult> DeleteTicket(long ticketId)
        {
            if (_ticketRepository.IsContextNull()|| _context.Responses == null)
            {
                return NotFound();
            }

            var ticket = _ticketRepository.FindById(ticketId);

            if (ticket == null)
            {
                return NotFound();
            }

            _ticketRepository.RemoveTicket(ticket);

            var items = _context.Responses.Where(a => a.TicketId == ticketId).ToList();
            foreach (var item in items)
                _context.Responses.Remove(item);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("tickets/{ticketId}")]
        public async Task<ActionResult<Ticket>> PostResponse(long ticketId, CreateResponseRequest req)
        {
            if (_context.Responses == null || _ticketRepository.IsContextNull())
            {
                return NotFound();
            }

            var ticket = _ticketRepository.FindById(ticketId);

            if (ticket == null)
            {
                return BadRequest("ticketId not found");
            }

            // update number of responses and isChecked fields
            ticket.NumberOfResponses++;
            ticket.IsChecked = true;
            if (ticket.FirstResponseDate == null)
            {
                ticket.FirstResponseDate = DateTime.Now;
            }

            var userEmail = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;

            _ticketRepository.UpdateTicketAsync(ticket);

            var response = new Response
            {
                TicketId = ticketId,
                IdInTicket = ticket.NumberOfResponses,
                Writer = userEmail,
                Text = req.Text,
                CreationTime = DateTime.Now,
            };

            _context.Responses.Add(response);
            await _context.SaveChangesAsync();

            return Ok(response);
        }

        [HttpGet("responses/{ticketId}")]
        public ActionResult<Ticket> GetResponses(long ticketId)
        {
            if (_context.Responses == null)
            {
                return NotFound();
            }

            var items = _context.Responses.Where(a => a.TicketId == ticketId).ToList();

            return Ok(items);
        }

    }
}
