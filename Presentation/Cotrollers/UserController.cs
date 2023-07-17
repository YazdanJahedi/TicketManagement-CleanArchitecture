using Application.Features.CreateResponse;
using Application.Features.CreateTicket;
using Application.Repository;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase                         
    {

        private readonly ITicketsRepository _ticketRepository;
        private readonly IResponsesRepository _responsesRepository;
        private readonly IFAQCategoriesRepository _faqCategoriesRepository;
        private readonly IFAQItemsRepository _faqItemsRepository;


        public UserController(ITicketsRepository ticketRepository, 
                              IResponsesRepository responsesRepository,
                              IFAQCategoriesRepository fAQCategoriesRepository,
                              IFAQItemsRepository fAQItemsRepository
                              )
        {
            _ticketRepository = ticketRepository;
            _responsesRepository = responsesRepository;
            _faqCategoriesRepository = fAQCategoriesRepository;
            _faqItemsRepository = fAQItemsRepository; 
        }


        [HttpGet("request/FAQ")]
        public async Task<ActionResult<IEnumerable<FAQCategory>>> GetFAQCategories()
        {
            if (_faqCategoriesRepository.IsContextNull())
            {
                return NotFound();
            }

            return await _faqCategoriesRepository.GetAll();
        }

        [HttpGet("request/FAQ/{id}")]
        public ActionResult<IEnumerable<FAQItem>> GetFAQItems(int id)
        {
            if (_faqItemsRepository.IsContextNull())
            {
                return NotFound();
            }

            var items = _faqItemsRepository.FindAllByCategoryId(id).ToList();

            return Ok(items);
        }


        [HttpPost("request/tickets")]
        public ActionResult<Ticket> PostTicket(CreateTicketRequest req)
        {
            if (_ticketRepository.IsContextNull())
            {
                return NotFound();
            }
            
            // check validation
            CreateTicketValidator validator = new();
            var validatorResult = validator.Validate(req);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            // extract id from token
            var id = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var userId = Convert.ToInt64(id);

            // create a new ticket instance
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

            _ticketRepository.AddTicketAsync(ticket);
            return Ok(ticket);
        }


        [HttpGet("request/tickets")]
        public ActionResult<Ticket> GetTickes()
        {
            if (_ticketRepository.IsContextNull())
            {
                return NotFound();
            }

            // extract user id from token
            var id = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var userId = Convert.ToInt64(id);

            // find all relative tickets
            var items = _ticketRepository.FindAllById(userId).ToList();
            return Ok(items);
        }


        [HttpPost("request/tickets/{ticketId}")]
        public async Task<ActionResult<Ticket>> PostResponse(long ticketId, CreateResponseRequest req)
        {
            if (_responsesRepository.IsContextNull() || _ticketRepository.IsContextNull())
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
            var ticket = _ticketRepository.FindById(userId);

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

            // find all relative responses
            var items = _responsesRepository.FindAllResonsesByTicketId(ticketId).ToList();
            return Ok(items);
        }
    }
}
