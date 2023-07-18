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

        private readonly ITicketsRepository _ticketRepository;
        private readonly IResponsesRepository _responsesRepository;

        public AdminController(ITicketsRepository ticketRepository, IResponsesRepository responsesRepository)
        {
            _ticketRepository = ticketRepository;
            _responsesRepository = responsesRepository;
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
        public IActionResult DeleteTicket(long ticketId)
        {
            if (_ticketRepository.IsContextNull()|| _responsesRepository.IsContextNull())
            {
                return NotFound();
            }

            var ticket = _ticketRepository.FindById(ticketId);

            if (ticket == null)
            {
                return NotFound();
            }

            // _ticketRepository.RemoveTicket(ticket);

            // var items = _responsesRepository.FindAllResonsesByTicketId(ticketId).ToList();
            // _responsesRepository.RemoveListOfResponses(items);

            return Ok("done");
        }

        [HttpPost("tickets/{ticketId}")]
        public ActionResult<Ticket> PostResponse(long ticketId, CreateResponseRequest req)
        {
            if (_responsesRepository.IsContextNull() || _ticketRepository.IsContextNull())
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

            _responsesRepository.AddResponseAsync(response);
            return Ok(response);
        }

        [HttpGet("responses/{ticketId}")]
        public ActionResult<Ticket> GetResponses(long ticketId)
        {
            if (_responsesRepository.IsContextNull())
            {
                return NotFound();
            }

            var items = _responsesRepository.FindAllResonsesByTicketId(ticketId).ToList();

            return Ok(items);
        }

    }
}
