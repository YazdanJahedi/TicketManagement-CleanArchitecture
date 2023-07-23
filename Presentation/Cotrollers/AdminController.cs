﻿using Application.DTOs;
using Application.Repository;
using Domain.Entities;
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

        private readonly ITicketsRepository _ticketRepository;
        private readonly IMessagesRepository _messagesRepository;

        public AdminController(ITicketsRepository ticketRepository, IMessagesRepository responsesRepository)
        {
            _ticketRepository = ticketRepository;
            _messagesRepository = responsesRepository;
        }

        [HttpGet("tickets")]
        public ActionResult<Ticket> GetTickets()
        {
            var items = _ticketRepository.GetAll();
            return Ok(items);
        }

        [HttpDelete("{ticketId}")]
        public IActionResult DeleteTicket(long ticketId)
        {

            var ticket = _ticketRepository.FindById(ticketId);

            if (ticket == null)
            {
                return NotFound();
            }

            _messagesRepository.RemoveAllByTicketId(ticketId);
            _ticketRepository.Remove(ticket);

            return Ok("done");
        }

        [HttpPost("tickets/{ticketId}")]
        public ActionResult<Message> PostMessage(long ticketId, CreateMessageDto req)
        {

            var ticket = _ticketRepository.FindById(ticketId);

            if (ticket == null)
            {
                return BadRequest("ticketId not found");
            }

            // update first response date for the first response
            if (ticket.FirstResponseDate == null)
            {
                ticket.FirstResponseDate = DateTime.Now;
            }

            var userIdString = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var userId = Convert.ToInt64(userIdString);
            var userEmail = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            if (userEmail == null) return BadRequest("Email not found");

            var message = new Message
            {
                TicketId = ticketId,
                CreatorEmail = userEmail,
                Text = req.Text,
                CreationDate = DateTime.Now,
            };

            _messagesRepository.Add(message);
            return Ok(message);
        }

        [HttpGet("messages/{ticketId}")]
        public ActionResult<Message> GetMessages(long ticketId)
        {

            var items = _messagesRepository.FindAllByTicketId(ticketId);
            return Ok(items);
        }

    }
}
