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

        [HttpGet("tickets")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            try
            {
                var respone = await _mediator.Send(new GetTicketsListRequest());
                return Ok(respone);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
            }
        }


        //[HttpGet("tickets/{userName}")]
        //public async Task<ActionResult<IEnumerable<Ticket>>> GetSearchedTickets(string userName)
        //{
        //    try
        //    {
        //        var respone = await _mediator.Send(new GetTicketsListRequest(userName));
        //        return Ok(respone);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
        //    }
        //}

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
