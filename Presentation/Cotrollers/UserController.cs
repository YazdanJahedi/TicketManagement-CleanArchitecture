﻿using Application.DTOs;
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


        [HttpPost("request/messages")]
        public async Task<ActionResult> PostMessage(CreateMessageRequest req)
        {
            try
            {
                var response = await _mediator.Send(req);
                return Ok();
            } catch (Exception ex)
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

    }
}
