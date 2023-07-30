/*using Application.DTOs.MessageDtos;
using Application.DTOs.TicketDtos;
using Application.Features.CreateResponse;
using Application.Features.CreateTicket;
using Application.Repository;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class UserController : ControllerBase
    {

        private readonly ITicketsRepository _ticketRepository;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IFAQCategoriesRepository _faqCategoriesRepository;
        private readonly IFAQItemsRepository _faqItemsRepository;
        private readonly IUsersRepository _usersRepository;


        public UserController(ITicketsRepository ticketRepository,
                              IMessagesRepository responsesRepository,
                              IFAQCategoriesRepository fAQCategoriesRepository,
                              IFAQItemsRepository fAQItemsRepository,
                              IUsersRepository usersRepository
                              )
        {
            _ticketRepository = ticketRepository;
            _messagesRepository = responsesRepository;
            _faqCategoriesRepository = fAQCategoriesRepository;
            _faqItemsRepository = fAQItemsRepository;
            _usersRepository = usersRepository;
        }


        [HttpGet("requestttttttttttttttttttt/FAQ")]
        public async Task<ActionResult<IEnumerable<FAQCategory>>> GetFAQCategories()
        {
            //var items = _faqCategoriesRepository.GetAllAsync();

            //
            var i = await _faqCategoriesRepository.TestMethod();

            return Ok(i);
        }


        [HttpGet("request/FAQ/{id}")]
        public ActionResult<IEnumerable<FAQItem>> GetFAQItems(int id)
        {
            var items = _faqItemsRepository.FindAllByCategoryIdAsync(id);

            return Ok(items);
        }


        [HttpPost("request/tickets")]
        public ActionResult<Ticket> PostTicket(CreateTicketDto req)
        {
            // check validation
            if (!CreateTicketValidator.IsValid(req))
            {
                return BadRequest("validation error: Title can not be empty");
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

            _ticketRepository.AddAsync(ticket);
            return Ok(ticket);
        }


        [HttpGet("request/tickets")]
        public ActionResult<IEnumerable<Ticket>> GetTickes()
        {
            // extract user id from token
            var id = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var userId = Convert.ToInt64(id);

            // find all relative tickets
            var ticketItems = _ticketRepository.FindAllByCreatorId(userId);

            // map to response model
            var ticketResponseItems = ticketItems.Select(t => new TicketResponseDto
            {
                Id = t.Id,
                CreatorId = t.CreatorId,
                Title = t.Title,
                Description = t.Description,
                FirstResponseDate = t.FirstResponseDate,
                CloseDate = t.CloseDate,
                IsChecked = CommonMethods.CalculateIsCheckedField(t.Id, _usersRepository, _messagesRepository),
                CreationDate = t.CreationDate,
            });

            return Ok(ticketResponseItems);
        }


        [HttpPost("request/tickets/{ticketId}")]
        public ActionResult<Ticket> PostResponse(long ticketId, CreateMessageDto req)
        {
            // check validation
            if (!CreateMessageValidator.IsValid(req))
            {
                return BadRequest("validation error: Title can not be empty");
            }

            var userIdString = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var userId = Convert.ToInt64(userIdString);

            // check access validation to ticket
            var ticket = _ticketRepository.FindById(ticketId);
            if (ticket == null)
            {
                return BadRequest("ticketId not found");
            }
            if (ticket.CreatorId != userId)
            {
                return BadRequest("You do not have access to entered ticket");
            }

            var userEmail = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            if (userEmail == null) return BadRequest("Email not found");

            var response = new Message
            {
                TicketId = ticketId,
                CreatorEmail = userEmail,
                Text = req.Text,
                CreationDate = DateTime.Now,
            };

            _messagesRepository.AddAsync(response);
            return Ok(response);
        }


        [HttpGet("messages/{ticketId}")]
        public ActionResult<IEnumerable<Ticket>> GetMessages(long ticketId)
        {
            var userIdString = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var userId = Convert.ToInt64(userIdString);

            // check access validation to ticket
            var ticket = _ticketRepository.FindById(ticketId);
            if (ticket == null)
            {
                return BadRequest("ticketId not found");
            }
            if (ticket.CreatorId != userId)
            {
                return BadRequest("You do not have access to entered ticket");
            }

            var items = _messagesRepository.FindAllByTicketIdAsync(ticketId);
            return Ok(items);
        }

    }
}
*/