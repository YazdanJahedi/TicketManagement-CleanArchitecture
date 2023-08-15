using Application.Dtos.MessageDtos;
using Application.Dtos.TicketDtos;
using Application.Interfaces.Service;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {

        private readonly ITicketService _ticketService; 
        private readonly IMessageService _messageService;
        private readonly IMessageAttachmentService _messageAttachmentService;

        public AdminController(ITicketService ticketService, IMessageService messageService,
                               IMessageAttachmentService messageAttachmentService)
        {
            _ticketService = ticketService;
            _messageService = messageService;
            _messageAttachmentService = messageAttachmentService;
        }

        [HttpGet("tickets")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets(int numberOfReturningTickets)
        {
            try
            {
                var respone = await _ticketService.GetAll(numberOfReturningTickets);
                return Ok(respone);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("tickets/{username}")]
        public async Task<ActionResult<IEnumerable<GetTicketsListResponse>>> GetUserTicketsList(string username)
        {
            try
            {
                var respone = await _ticketService.GetAllByUser(username);
                return Ok(respone);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("tickt/{ticketId}")]
        public async Task<ActionResult<GetTicketResponse>> GetTicket(long ticketId)
        {
            try
            {
                var respones = await _ticketService.Get(ticketId);
                return Ok(respones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("tickets/close/{ticketId}")]
        public async Task<ActionResult> CloseTicket(long ticketId)
        {
            try
            {
                await _ticketService.Close(ticketId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpDelete("tickets/{ticketId}")]
        public async Task<ActionResult> DeleteTicket(long ticketId)
        {
            try
            {
                await _ticketService.Remove(ticketId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("messages")]
        public async Task<ActionResult> PostMessage([FromForm] CreateMessageRequest req)
        {
            try 
            {
                await _messageService.Add(req);
                return Ok();
            } 
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("messages/download/{fileId}")]
        public async Task<ActionResult> Download(long fileId)
        {
            try
            {
                var response = await _messageAttachmentService.Download(fileId);
                return File(response.Data, response.MimeType, response.FileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
