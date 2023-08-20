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

        private readonly IUnitOfSevice _unitOfSevice;

        public UserController(IUnitOfSevice unitOfSevice)
        {
            _unitOfSevice = unitOfSevice;
        }


        [HttpGet]
        [Route("request/tickets")]
        public async Task<ActionResult<IEnumerable<GetTicketsListResponse>>> GetTickes(int first, int last)
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
        [Route("request/tickets/{ticketId}")]
        public async Task<ActionResult<GetTicketResponse>> GetTicket(long ticketId)
        {
            try
            {
                var response = await _unitOfSevice.TicketService.Get(ticketId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        [Route("request/tickets")]
        public async Task<ActionResult> PostTicket([FromForm] CreateTicketRequest req)
        {
            try
            {
                await _unitOfSevice.TicketService.Add(req);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        [Route("request/messages")]
        public async Task<ActionResult> PostMessage([FromForm] CreateMessageRequest req)
        {
            try
            {
                await _unitOfSevice.MessageService.Add(req);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("request/messages/download/{fileId}")]
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


        [HttpGet]
        [Route("request/FAQ")]
        public async Task<ActionResult<IEnumerable<GetFaqCategoriesResponse>>> GetFAQCategories()
        {
            try
            {
                var respose = await _unitOfSevice.FaqService.GetFaqCategories();
                return Ok(respose);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet]
        [Route("request/FAQ/{id}")]
        public async Task<ActionResult<IEnumerable<GetFaqItemsResponse>>> GetFAQItems(long id)
        {
            try
            {
                var response = await _unitOfSevice.FaqService.GetFaqItems(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

    }
}
