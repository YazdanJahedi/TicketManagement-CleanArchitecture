using Application.DTOs;
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
        private readonly IMessagesRepository _messagesRepository;
        private readonly IFAQCategoriesRepository _faqCategoriesRepository;
        private readonly IFAQItemsRepository _faqItemsRepository;


        public UserController(ITicketsRepository ticketRepository, 
                              IMessagesRepository responsesRepository,
                              IFAQCategoriesRepository fAQCategoriesRepository,
                              IFAQItemsRepository fAQItemsRepository
                              )
        {
            _ticketRepository = ticketRepository;
            _messagesRepository = responsesRepository;
            _faqCategoriesRepository = fAQCategoriesRepository;
            _faqItemsRepository = fAQItemsRepository; 
        }


        [HttpGet("request/FAQ")]
        public async Task<IEnumerable<FAQCategory>> GetFAQCategories()
        {
            _faqCategoriesRepository.CheckNull();

            return await _faqCategoriesRepository.GetAllAsync();
        }

        [HttpGet("request/FAQ/{id}")]
        public ActionResult<IEnumerable<FAQItem>> GetFAQItems(int id)
        {
            _faqItemsRepository.CheckNull();

            var items = _faqItemsRepository.FindAllByCategoryId(id);

            return Ok(items);
        }


        [HttpPost("request/tickets")]
        public ActionResult<Ticket> PostTicket(CreateTicketDto req)
        {
            _ticketRepository.CheckNull();
            
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
                CreatorId = userId,
                Title = req.Title,
                Description = req.Description,
                CreationDate = DateTime.Now,
                FirstResponseDate = null,
                CloseDate = null,
            };

            _ticketRepository.Add(ticket);
            return Ok(ticket);
        }


        [HttpGet("request/tickets")]
        public ActionResult<Ticket> GetTickes()
        {
            _ticketRepository.CheckNull();

            // extract user id from token
            var id = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var userId = Convert.ToInt64(id);

            // find all relative tickets
            var items = _ticketRepository.FindAllByCreatorId(userId).ToList();
            return Ok(items);
        }


        [HttpPost("request/tickets/{ticketId}")]
        public ActionResult<Ticket> PostResponse(long ticketId, CreateMessageDto req)
        {
            _ticketRepository.CheckNull();
            _messagesRepository.CheckNull();

            CreateMessageValidator validator = new();
            var validatorResult = validator.Validate(req);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            var userIdString = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var userId = Convert.ToInt64(userIdString);
            var ticket = _ticketRepository.FindById(ticketId);

            if (ticket == null)
            {
                return BadRequest("ticketId not found");
            }
            if (ticket.CreatorId != userId)
            {
                return BadRequest("You do not have access to entered ticket");
            }

            // update number of responses and isChecked fields
            //ticket.NumberOfResponses++;
            //ticket.IsChecked = false;

            var response = new Message
            {
                TicketId = ticketId,
                CreatorId = userId,
                Text = req.Text,
                CreationDate = DateTime.Now,
            };

            _messagesRepository.Add(response);
            return Ok(response);
        }

        [HttpGet("messages/{ticketId}")]
        public ActionResult<Ticket> GetMessages(long ticketId)
        {
            _messagesRepository.CheckNull();

            // find all relative responses
            var items = _messagesRepository.FindAllByTicketId(ticketId);
            return Ok(items);
        }
    }
}
