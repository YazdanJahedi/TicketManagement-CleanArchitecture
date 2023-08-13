using Application.Common.DTOs;
using Application.Dtos.FaqDtos;
using Application.Features.MessageAttachmentFeatures.DownloadFile;
using Application.Features.MessageFeatures.CreateMessage;
using Application.Features.TicketFeatures.CreateTicket;
using Application.Features.TicketFeatures.GetTicket;
using Application.Features.TicketFeatures.GetTicketsList;
using Application.Features.TicketFeatures.GetTicketsList.GetAllTicketsList;
using Application.Interfaces.Service;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly IFaqService _faqService;

        public UserController(IFaqService faqService)
        {
            _faqService = faqService;
        }


        [HttpGet("request/tickets")]
        public async Task<ActionResult<IEnumerable<GetTicketsListResponse>>> GetTickes(int numberOfReturningTickets)
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

        [HttpGet("request/tickets/{ticketId}")]
        public async Task<ActionResult<GetTicketResponse>> GetTicket(long ticketId)
        {
            try
            {
                var response = await _mediator.Send(new GetTicketRequest(ticketId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
            }
        }


        [HttpPost("request/tickets")]
        public async Task<ActionResult> PostTicket([FromForm] CreateTicketRequest req)
        {
            try
            {
                await _mediator.Send(req);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
            }
        }


        [HttpPost("request/messages")]
        public async Task<ActionResult> PostMessage([FromForm] CreateMessageRequest req)
        {
            try
            {
                await _mediator.Send(req);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
            }
        }

        [HttpPost("request/messages/download")]
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
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
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
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
            }

        }

    }
}
