using Application.DTOs;
using Application.DTOs.MessageDtos;
using Application.DTOs.TicketDtos;
using Application.Repository;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

/*        [HttpGet("tickets")]
        public ActionResult<IEnumerable<Ticket>> GetTickets()
        {
            // get all items
            var ticketItems = _ticketRepository.GetAllAsync();

            // create response 
            var ticketResponseItems = ticketItems.Select(t =>
                    new TicketResponseDto
                    {
                        Id = t.Id,
                        CreatorId = t.CreatorId,
                        Title = t.Title,
                        Description = t.Description,
                        FirstResponseDate = t.FirstResponseDate,
                        CloseDate = t.CloseDate,
                        IsChecked = CommonMethods.CalculateIsCheckedField(t.Id, _usersRepository, _messagesRepository),
                        CreationDate = t.CreationDate,
                    }
                );

            return Ok(ticketItems);
        }*/

        [HttpDelete("{ticketId}")]
        public async Task<ActionResult> DeleteTicket(long ticketId)
        {
            try
            {
                await _mediator.Send(new DeleteTicketRequest(ticketId));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
            }
        }


        [HttpPost("GetFullTicket/{ticketId}")]
        public async Task<ActionResult<GetTicketResponse>> GetFullTicket(long ticketId)
        {
            try
            {
                var response = await _mediator.Send(new GetTicketRequest(ticketId));
                return Ok(response);
            }catch (Exception ex)
            {
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
            }
        }

 /*       [HttpPost("tickets/{ticketId}")]
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

            _messagesRepository.AddAsync(message);
            return Ok(message);
        }

        [HttpGet("messages/{ticketId}")]
        public ActionResult<IEnumerable<Message>> GetMessages(long ticketId)
        {

            var items = _messagesRepository.FindAllByTicketIdAsync(ticketId);
            return Ok(items);
        }

        [HttpGet("userInfo")]
        public ActionResult<User> GetUsreInformation(string email)
        {
            var user = _usersRepository.FindByEmail(email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return user;
        }*/
    }
}
