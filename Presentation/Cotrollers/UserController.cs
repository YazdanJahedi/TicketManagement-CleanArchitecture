using Application.DTOs;
using Application.DTOs.FaqCategoryDto;
using Application.DTOs.FaqItemsDto;
using Application.DTOs.MessageDtos;
using Application.DTOs.TicketDtos;
using Application.Features.FaqCategoryFeatures.Queries;
using Application.Features.FaqItemFeatures.Queries;
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
            }catch (Exception ex)
            {
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
            }

        }


        [HttpPost("request/tickets")]
        public async Task<ActionResult<CreateTicketResponse>> PostTicket(CreateTicketRequest req)
        {
            try
            {
                var response = await _mediator.Send(req);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
            }
        }


        //    [HttpGet("request/tickets")]
        //    public ActionResult<IEnumerable<Ticket>> GetTickes()
        //    {
        //        // extract user id from token
        //        var v = User.Claims;
        //        User.
        //        var id = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
        //        var userId = Convert.ToInt64(id);

        //        // find all relative tickets
        //        var ticketItems = _ticketRepository.FindAllByCreatorId(userId);

        //        // map to response model
        //        var ticketResponseItems = ticketItems.Select(t => new CreateTicketResponse
        //        {
        //            Id = t.Id,
        //            CreatorId = t.CreatorId,
        //            Title = t.Title,
        //            FirstResponseDate = t.FirstResponseDate,
        //            //IsChecked = CommonMethods.CalculateIsCheckedField(t.Id, _usersRepository, _messagesRepository),
        //            CreationDate = t.CreationDate,
        //            FaqCategoryId = 1,
        //        });

        //        return Ok(ticketResponseItems);
        //    }


        //    [HttpPost("request/tickets/{ticketId}")]
        //    public ActionResult<Ticket> PostResponse(long ticketId, CreateMessageDto req)
        //    {
        //        // check validation
        //        if (!CreateMessageValidator.IsValid(req))
        //        {
        //            return BadRequest("validation error: Title can not be empty");
        //        }

        //        var userIdString = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
        //        var userId = Convert.ToInt64(userIdString);

        //        // check access validation to ticket
        //        var ticket = _ticketRepository.FindById(ticketId);
        //        if (ticket == null)
        //        {
        //            return BadRequest("ticketId not found");
        //        }
        //        if (ticket.CreatorId != userId)
        //        {
        //            return BadRequest("You do not have access to entered ticket");
        //        }

        //        var userEmail = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
        //        if (userEmail == null) return BadRequest("Email not found");

        //        var response = new Message
        //        {
        //            TicketId = ticketId,
        //            CreatorId = userId,
        //            Text = req.Text,
        //            CreationDate = DateTime.Now,
        //        };

        //        _messagesRepository.AddAsync(response);
        //        return Ok(response);
        //    }


        [HttpGet("request/getTicket/{ticketId}")]
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

    }
}
