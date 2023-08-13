using Application.Common.DTOs;
using Application.Dtos.MessageDtos;
using Application.Dtos.TicketDtos;
using Application.Features.MessageAttachmentFeatures.DownloadFile;
using Application.Features.TicketFeatures.CloseTicket;
using Application.Features.TicketFeatures.DeleteTicket;
using Application.Features.TicketFeatures.GetTicketsList.GetAllTicketsList;
using Application.Features.TicketFeatures.GetTicketsList.GetUserTicketsList;
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
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets(int numberOfReturningTickets)
        {
            try
            {
                var respone = await _mediator.Send(new GetTicketsListRequest(numberOfReturningTickets));
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


        [HttpPost("tickets/close/{ticketId}")]
        public async Task<ActionResult> CloseTicket(long ticketId)
        {
            try
            {
                await _mediator.Send(new CloseTicketRequest(ticketId));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
            }
        }


        [HttpDelete("tickets/{ticketId}")]
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

        [HttpPost("messages")]
        public async Task<ActionResult> PostMessage([FromForm] CreateMessageRequest req)
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


        [HttpPost("messages/download")]
        public async Task<ActionResult> Download(DownloadFileRequest req)
        {
            try
            {
                var response = await _mediator.Send(req);
                return File(response.FileData, response.MimeType, response.FileName);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
            }
        }


    }
}
