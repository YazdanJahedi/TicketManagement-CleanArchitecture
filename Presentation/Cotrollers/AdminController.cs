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

        private readonly IUnitOfSevice _unitOfSevice;

        public AdminController(IUnitOfSevice unitOfSevice)
        {
            _unitOfSevice = unitOfSevice;
        }

        [HttpGet]
        [Route("tickets")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets(int first, int last)
        {
            try
            {
                var respone = await _unitOfSevice.TicketService.GetAll(first, last);
                return Ok(respone);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet]
        [Route("tickets/{username}")]
        public async Task<ActionResult<IEnumerable<GetTicketsListResponse>>> GetUserTicketsList(string username, int first, int last)
        {
            try
            {
                var respone = await _unitOfSevice.TicketService.GetAllByUser(username, first, last);
                return Ok(respone);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("tickt/{ticketId}")]
        public async Task<ActionResult<GetTicketResponse>> GetTicket(long ticketId)
        {
            try
            {
                var respones = await _unitOfSevice.TicketService.Get(ticketId);
                return Ok(respones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        [Route("tickets/close/{ticketId}")]
        public async Task<ActionResult> CloseTicket(long ticketId)
        {
            try
            {
                await _unitOfSevice.TicketService.Close(ticketId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpDelete]
        [Route("tickets/{ticketId}")]
        public async Task<ActionResult> DeleteTicket(long ticketId)
        {
            try
            {
                await _unitOfSevice.TicketService.Remove(ticketId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("messages")]
        public async Task<ActionResult> PostMessage([FromForm] CreateMessageRequest req)
        {
            try 
            {
                await _unitOfSevice.MessageService.Add(req);
                return Ok();
            } 
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        [Route("messages/download/{fileId}")]
        public async Task<ActionResult> Download(long fileId)
        {
            try
            {
                var response = await _unitOfSevice.MessageAttachmentService.Download(fileId);
                return File(response.Data, response.MimeType, response.FileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
