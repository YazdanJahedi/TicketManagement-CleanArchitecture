﻿using Application.DTOs;
using Application.DTOs.Ticket;
using Application.Features.CreateResponse;
using Application.Features.CreateTicket;
using Application.Repository;
using Domain.Entities;
using FluentValidation;
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

        private readonly ITicketsRepository _ticketRepository;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IFAQCategoriesRepository _faqCategoriesRepository;
        private readonly IFAQItemsRepository _faqItemsRepository;
        private readonly IUsersRepository _usersRepository;


        public UserController(ITicketsRepository ticketRepository, 
                              IMessagesRepository responsesRepository,
                              IFAQCategoriesRepository fAQCategoriesRepository,
                              IFAQItemsRepository fAQItemsRepository,
                              IUsersRepository usersRepository
                              )
        {
            _ticketRepository = ticketRepository;
            _messagesRepository = responsesRepository;
            _faqCategoriesRepository = fAQCategoriesRepository;
            _faqItemsRepository = fAQItemsRepository;
            _usersRepository = usersRepository;
        }


        [HttpGet("request/FAQ")]
        public  IEnumerable<FAQCategory> GetFAQCategories()
        {
            return _faqCategoriesRepository.GetAll();
        }

        [HttpGet("request/FAQ/{id}")]
        public ActionResult<IEnumerable<FAQItem>> GetFAQItems(int id)
        {
            var items = _faqItemsRepository.FindAllByCategoryId(id);

            return Ok(items);
        }


        [HttpPost("request/tickets")]
        public ActionResult<Ticket> PostTicket(CreateTicketDto req)
        {            
            // check validation
            CreateTicketValidator validator = new();
            var validatorResult = validator.Validate(req);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            // extract id from token
            var id = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var userId = Convert.ToInt64(id);

            // create a new ticket instance
            var ticket = new Ticket
            {
                CreatorId = userId,
                Title = req.Title,
                Description = req.Description,
                CreationDate = DateTime.Now,
                FirstResponseDate = null,
                CloseDate = null,
            };

            _ticketRepository.Add(ticket);
            return Ok(ticket);
        }


        [HttpGet("request/tickets")]
        public ActionResult<Ticket> GetTickes()
        {
            // extract user id from token
            var id = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var userId = Convert.ToInt64(id);

            // find all relative tickets
            var items = _ticketRepository.FindAllByCreatorId(userId).ToList();

            // Map to anther model
            var resp = items.Select(t => new TicketResponse
            {
                Id = t.Id,
                CreatorId = t.CreatorId,
                Title = t.Title,
                Description = t.Description,
                FirstResponseDate = t.FirstResponseDate,
                CloseDate = t.CloseDate,
                IsChecked = CalculateIsCheckedField(t.Id),
                CreationDate = t.CreationDate,
            });
            return Ok(resp);
        }


        [HttpPost("request/tickets/{ticketId}")]
        public ActionResult<Ticket> PostResponse(long ticketId, CreateMessageDto req)
        {

            CreateMessageValidator validator = new();
            var validatorResult = validator.Validate(req);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            var userIdString = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var userId = Convert.ToInt64(userIdString);
            var ticket = _ticketRepository.FindById(ticketId);

            if (ticket == null)
            {
                return BadRequest("ticketId not found");
            }
            if (ticket.CreatorId != userId)
            {
                return BadRequest("You do not have access to entered ticket");
            }

            var userEmail = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            if (userEmail == null) return BadRequest("Email not found");

            var response = new Message
            {
                TicketId = ticketId,
                CreatorEmail= userEmail,
                Text = req.Text,
                CreationDate = DateTime.Now,
            };

            _messagesRepository.Add(response);
            return Ok(response);
        }

        [HttpGet("messages/{ticketId}")]
        public ActionResult<Ticket> GetMessages(long ticketId)
        {
            // find all relative responses
            var items = _messagesRepository.FindAllByTicketId(ticketId);
            return Ok(items);
        }


        private bool CalculateIsCheckedField(long ticketId)
        {
            var message = _messagesRepository.FindLastMessageByTicketId(ticketId);
            if (message == null) return false;
            var user = _usersRepository.FindByEmail(message.CreatorEmail);
            if (user == null) return false;
            return user.Role.Equals("Admin");
        }
    }
}
