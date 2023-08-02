using Application.DTOs;
using Application.DTOs.Common;
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

        [HttpPost("message")]
        public async Task<ActionResult> PostMessage(CreateMessageRequest req)
        {
            try 
            {
                var response = await _mediator.Send(req);
                return Ok();
            } 
            catch (Exception ex)
            {
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
            }
        }

        /*
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
