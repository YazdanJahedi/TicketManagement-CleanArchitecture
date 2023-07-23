using Application.DTOs;
using Application.DTOs.Ticket;
using Application.Repository;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase                              
    {

        private readonly ITicketsRepository _ticketRepository;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IUsersRepository _usersRepository;
        public AdminController(ITicketsRepository ticketRepository, 
                               IMessagesRepository responsesRepository, 
                               IUsersRepository usersRepository)
        {
            _ticketRepository = ticketRepository;
            _messagesRepository = responsesRepository;
            _usersRepository = usersRepository;
        }

        [HttpGet("tickets")]
        public ActionResult<Ticket> GetTickets()
        {
            var ticketItems = _ticketRepository.GetAll();
            var ticketResponseItems = ticketItems.Select(t =>
            new TicketResponse
            {
                Id = t.Id,
                CreatorId = t.CreatorId,
                Title = t.Title,
                Description = t.Description,
                FirstResponseDate = t.FirstResponseDate,
                CloseDate = t.CloseDate,
                IsChecked = CommonMethods.CalculateIsCheckedField(t.Id,_usersRepository, _messagesRepository),
                CreationDate = t.CreationDate,
            });
            return Ok(ticketResponseItems);
        }

        [HttpDelete("{ticketId}")]
        public IActionResult DeleteTicket(long ticketId)
        {

            var ticket = _ticketRepository.FindById(ticketId);

            if (ticket == null)
            {
                return NotFound("Ticket not found");
            }

            _messagesRepository.RemoveAllByTicketId(ticketId);
            _ticketRepository.Remove(ticket);

            return Ok("done");
        }

        [HttpPost("tickets/{ticketId}")]
        public ActionResult<Message> PostMessage(long ticketId, CreateMessageDto req)
        {

            var ticket = _ticketRepository.FindById(ticketId);

            if (ticket == null)
            {
                return BadRequest("ticketId not found");
            }

            // Update first_response_date for the first response
            if (ticket.FirstResponseDate == null)
            {
                ticket.FirstResponseDate = DateTime.Now;              
            }

            var userEmail = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            if (userEmail == null) return BadRequest("Email not found");

            var message = new Message
            {
                TicketId = ticketId,
                CreatorEmail = userEmail,
                Text = req.Text,
                CreationDate = DateTime.Now,
            };

            _messagesRepository.Add(message);
            return Ok(message);
        }

        [HttpGet("messages/{ticketId}")]
        public ActionResult<Message> GetMessages(long ticketId)
        {

            var items = _messagesRepository.FindAllByTicketId(ticketId);
            return Ok(items);
        }


        [HttpGet("userInfo/{email}")]
        public ActionResult<User> GetUsreInformation(string email)
        {
            var user = _usersRepository.FindByEmail(email);
            if(user == null) return NotFound("User not found");
            return user;
        }
    }
}
