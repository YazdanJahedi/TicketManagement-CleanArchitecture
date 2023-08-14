using Application.Common.Exceptions;
using Application.Dtos.TicketDtos;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;


namespace Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketsRepository _ticketsRepository;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public TicketService(ITicketsRepository ticketsRepository,
                             IMessagesRepository messagesRepository,
                             IAuthService userService,
                             IMapper mapper)
        {
            _ticketsRepository = ticketsRepository;
            _messagesRepository = messagesRepository;
            _authService = userService;
            _mapper = mapper;
        }

        public async Task Add(CreateTicketRequest request)
        {
            var claim = _authService.GetClaims();

            // Create a new ticket instance
            var ticket = new Ticket
            {
                CreatorId = claim.Id,
                Title = request.Title,
                CreationDate = DateTime.Now,
                FaqCategoryId = request.FaqCatgoryId,
                Status = TicketStatus.NotChecked,
            };

            await _ticketsRepository.AddAsync(ticket);

            var firstMesage = new Message
            {
                TicketId = ticket.Id,
                CreatorId = claim.Id,
                CreationDate = DateTime.Now,
                Text = request.Description,

            };

            await _messagesRepository.AddAsync(firstMesage);
            //if (request.Attachments != null)
            //    await _messageAttachmentService.SaveMultipeAttachments(request.Attachments, firstMesage.Id);

        }

        public async Task Close(long ticketId)
        {
            var ticket = await _ticketsRepository.FindByIdAsync(ticketId);
            if (ticket == null) throw new DirectoryNotFoundException("Ticket not found");

            if (ticket.Status == TicketStatus.Closed)
            {
                ticket.Status = TicketStatus.Checked;
                ticket.CloseDate = null;
            }
            else
            {
                ticket.Status = TicketStatus.Closed;
                ticket.CloseDate = DateTime.Now;
            }

            await _ticketsRepository.UpdateAsync(ticket);
        }

        public async Task<GetTicketResponse> Get(long ticketId)
        {
            var claims = _authService.GetClaims();

            var ticket = await _ticketsRepository.FindByIdAsync(ticketId);
            if (ticket == null || claims.Role == "User" && ticket.Creator!.Id != claims.Id) throw new NotFoundException("Ticket not found");

            var response = _mapper.Map<GetTicketResponse>(ticket);
            return response;
        }

        public async Task<IEnumerable<GetTicketsListResponse>> GetAll(int number)
        {
            var claims = _authService.GetClaims();

            IEnumerable<Ticket> tickets;
            if (claims.Role == "Admin")
                tickets = await _ticketsRepository.GetAllFirstOnesAsync(number);
            else // role == "User"
                tickets = await _ticketsRepository.GetFirstOnesByCreatorIdAsync(claims.Id, number);

            var response = _mapper.Map<IEnumerable<GetTicketsListResponse>>(tickets);

            return response;
        }

        public async Task<IEnumerable<GetTicketsListResponse>> GetAllByUser(string username)
        {
            // not checked
            var tickets = await _ticketsRepository.GetAllAsync(t => t.Creator!.Name == username, "Creator");
            if (tickets == null) throw new NotFoundException("no ticket found");

            var response = _mapper.Map<IEnumerable<GetTicketsListResponse>>(tickets);

            return response;
        }

        public async Task Remove(long ticketId)
        {
            var ticket = await _ticketsRepository.FindByIdAsync(ticketId);
            if (ticket == null) throw new NotFoundException("Ticket not found");

            await _ticketsRepository.RemoveAsync(ticket);
        }

        public async Task UpdateTicketAfterSendNewMessage(Ticket ticket, string creatorRole)
        {
            // fill status field and first-response-date
            bool ticketNeedUpdate = false;
            if (creatorRole == "User" && ticket.Status != TicketStatus.NotChecked)
            {
                ticket.Status = TicketStatus.NotChecked;
                ticketNeedUpdate = true;
            }
            else if (creatorRole == "Admin" && ticket.Status != TicketStatus.Checked) 
            {
                if (ticket.FirstResponseDate == null) ticket.FirstResponseDate = DateTime.Now;
                ticket.Status = TicketStatus.Checked;
                ticketNeedUpdate = true;
            }
            if (ticketNeedUpdate) await _ticketsRepository.UpdateAsync(ticket);
        }
    }
}
