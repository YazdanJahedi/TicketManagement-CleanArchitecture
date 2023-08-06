using Application.DTOs;
using Application.Features.FaqCategoryFeatures.GetAllCategories;
using Application.Features.FaqCategoryFeatures.Queries;
using Application.Features.FaqItemFeatures.GetAllFaqItems;
using Application.Features.FaqItemFeatures.Queries;
using Application.Features.MessageFeatures.CreateMessage;
using Application.Features.TicketFeatures.CreateTicket;
using Application.Features.TicketFeatures.GetTicket;
using Application.Features.TicketFeatures.GetTicketsList;
using Application.Features.TicketFeatures.GetTicketsList.GetAllTicketsList;
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

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("request/tickets")]
        public async Task<ActionResult<IEnumerable<GetTicketsListResponse>>> GetTickes()
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
        public async Task<ActionResult> PostTicket(CreateTicketRequest req)
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
        public async Task<ActionResult> PostMessage(CreateMessageRequest req)
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


        [HttpGet("request/FAQ")]
        public async Task<ActionResult<IEnumerable<GetFaqCategoriesResponse>>> GetFAQCategories()
        {
            try
            {
                var respose = await _mediator.Send(new GetFaqCategoriesRequest());
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
                var response = await _mediator.Send(new GetFaqItemsRequest(id));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
            }

        }

    }
}
