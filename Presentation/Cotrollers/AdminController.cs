using Application.DTOs;
using Application.Features.MessageFeatures.CreateMessage;
using Application.Features.TicketFeatures.CloseTicket;
using Application.Features.TicketFeatures.DeleteTicket;
using Application.Features.TicketFeatures.GetTicket;
using Application.Features.TicketFeatures.GetTicketsList;
using Application.Features.TicketFeatures.GetUserTicketsList;
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


        [HttpGet("tickets/{username}")]
        public async Task<ActionResult<IEnumerable<GetTicketsListResponse>>> GetUserTicketsList(string username)
        {
            try
            {
                var respone = await _mediator.Send(new GetUserTicketsListRequest(username));
                return Ok(respone);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
            }
        }

        [HttpGet("tickt/{ticketId}")]
        public async Task<ActionResult<GetTicketResponse>> GetTicket(long ticketId)
        {
            try
            {
                var respones = await _mediator.Send(new GetTicketRequest(ticketId));
                return Ok(respones);
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

        [HttpPost("tickets/close/{ticketId}")]
        public async Task<ActionResult> CloseTicket(long ticketId)
        {
            try
            {
                await _mediator.Send(new PostCloseTicketRequest(ticketId));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
            }
        }


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

    }
}
