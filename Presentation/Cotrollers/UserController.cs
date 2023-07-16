using Application.Features.CreateResponse;
using Application.Features.CreateTicket;
using Application.Repository;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase,
                                  IFAQRepository,
                                  IUTicketRepository,
                                  IResponseRepository
    {

        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("request/FAQ")]
        public async Task<ActionResult<IEnumerable<FAQCategory>>> GetFAQCategories()
        {
            if (_context.FAQCategories == null)
            {
                return NotFound();
            }
            return await _context.FAQCategories.ToListAsync();
        }

        [HttpGet("request/FAQ/{id}")]
        public ActionResult<IEnumerable<FAQItem>> GetFAQItems(int id)
        {
            if (_context.FAQItems == null)
            {
                return NotFound();
            }

            var items = _context.FAQItems.Where(a => a.CategoryId == id).ToList();

            return Ok(items);
        }


        [HttpPost("request/tickets")]
        public async Task<ActionResult<Ticket>> PostTicket(CreateTicketRequest req)
        {
            if (_context.Tickets == null)
            {
                return NotFound();
            }

            CreateTicketValidator validator = new();
            var validatorResult = validator.Validate(req);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            var id = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var userId = Convert.ToInt64(id);

            var ticket = new Ticket
            {
                UserId = userId,
                Title = req.Title,
                Description = req.Description,
                IsChecked = false,
                CreationTime = DateTime.Now,
                FirstResponseDate = null,
                CloseDate = null,
                NumberOfResponses = 0,
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return Ok(ticket);
        }


        [HttpGet("request/tickets")]
        public ActionResult<Ticket> GetTickes()
        {
            if (_context.Tickets == null)
            {
                return NotFound();
            }

            var id = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var userId = Convert.ToInt64(id);

            var items = _context.Tickets.Where(a => a.UserId == userId).ToList();
            return Ok(items);
        }


        [HttpPost("request/tickets/{ticketId}")]
        public async Task<ActionResult<Ticket>> PostResponse(long ticketId, CreateResponseRequest req)
        {
            if (_context.Responses == null || _context.Tickets == null)
            {
                return NotFound();
            }

            CreateResponseValidator validator = new();
            var validatorResult = validator.Validate(req);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            var userIdString = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var userId = Convert.ToInt64(userIdString);
            var ticket = _context.Tickets.Find(ticketId);

            if (ticket == null)
            {
                return BadRequest("ticketId not found");
            }
            if (ticket.UserId != userId)
            {
                return BadRequest("You do not have access to entered ticket");
            }

            // update number of responses and isChecked fields
            ticket.NumberOfResponses++;
            ticket.IsChecked = false;

            var userEmail = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

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
