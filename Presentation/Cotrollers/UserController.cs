using Application.Dtos.FaqDtos;
using Application.Dtos.MessageDtos;
using Application.Dtos.TicketDtos;
using Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly IFaqService _faqService;
        private readonly ITicketService _ticketService;
        private readonly IMessageAttachmentService _messageAttachmentService;
        private readonly IMessageService _messageService;

        public UserController(IFaqService faqService, ITicketService ticketService, IMessageAttachmentService messageAttachmentService, IMessageService messageService)
        {
            _faqService = faqService;
            _ticketService = ticketService;
            _messageAttachmentService = messageAttachmentService;
            _messageService = messageService;
        }


        [HttpGet("request/tickets")]
        public async Task<ActionResult<IEnumerable<GetTicketsListResponse>>> GetTickes(int numberOfReturningTickets)
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

        [HttpGet("request/tickets/{ticketId}")]
        public async Task<ActionResult<GetTicketResponse>> GetTicket(long ticketId)
        {
            try
            {
                var response = await _ticketService.Get(ticketId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("request/tickets")]
        public async Task<ActionResult> PostTicket([FromForm] CreateTicketRequest req)
        {
            try
            {
                await _ticketService.Add(req);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("request/messages")]
        public async Task<ActionResult> PostMessage([FromForm] CreateMessageRequest req)
        {
            try
            {
                await _messageService.Add(req);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("request/messages/download/{fileId}")]
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


        [HttpGet("request/FAQ")]
        public async Task<ActionResult<IEnumerable<GetFaqCategoriesResponse>>> GetFAQCategories()
        {
            try
            {
                var respose = await _faqService.GetFaqCategories();
                return Ok(respose);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("request/FAQ/{id}")]
        public async Task<ActionResult<IEnumerable<GetFaqItemsResponse>>> GetFAQItems(long id)
        {
            try
            {
                var response = await _faqService.GetFaqItems(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

    }
}
