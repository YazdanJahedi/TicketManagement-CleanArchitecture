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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageAttachmentService _messageAttachmentService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public TicketService(IUnitOfWork unitOfWork,
                             IMessageAttachmentService messageAttachmentService,
                             IAuthService userService,
                             IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _messageAttachmentService = messageAttachmentService;
            _authService = userService;
            _mapper = mapper;
        }

        public async Task Add(CreateTicketRequest request)
        {
            var claim = _authService.GetClaims();

            var ticket = new Ticket
            {
                CreatorId = claim.Id,
                Title = request.Title,
                CreationDate = DateTime.Now,
                FaqCategoryId = request.FaqCatgoryId,
                Status = TicketStatus.NotChecked,
            };
            var firstMessage = new Message
            {
                TicketId = ticket.Id,
                CreatorId = claim.Id,
                CreationDate = DateTime.Now,
                Text = request.Description,

            };

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    await _unitOfWork.TicketsRepository.AddAsync(ticket);
                    await _unitOfWork.MessagesRepository.AddAsync(firstMessage);
                    if (request.Attachments != null)
                        await _messageAttachmentService.UploadRange(request.Attachments, firstMessage.Id);
                    await _unitOfWork.SaveAsync();
                } 
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }

        public async Task<GetTicketResponse> Get(long ticketId)
        {
            var claims = _authService.GetClaims();

            var ticket = await _unitOfWork.TicketsRepository.FindByIdAsync(ticketId);
            if (ticket == null || claims.Role == "User" && ticket.Creator!.Id != claims.Id) throw new NotFoundException("Ticket not found");

            var response = _mapper.Map<GetTicketResponse>(ticket);
            return response;
        }

        public async Task<IEnumerable<GetTicketsListResponse>> GetAll(int number)
        {
            var claims = _authService.GetClaims();

            var tickets = claims.Role == "Admin" ?
                await _unitOfWork.TicketsRepository.GetAllAsync(number: number, includes: "Creator") :
                await _unitOfWork.TicketsRepository.GetAllAsync(number: number, condition: e => e.CreatorId== claims.Id, includes: "Creator");

            var response = _mapper.Map<IEnumerable<GetTicketsListResponse>>(tickets);

            return response;
        }

        public async Task<IEnumerable<GetTicketsListResponse>> GetAllByUser(string username)
        {
            var tickets = await _unitOfWork.TicketsRepository.GetAllAsync(condition: t => t.Creator!.Name == username, includes: "Creator");
            if (tickets == null) throw new NotFoundException("no ticket found");

            var response = _mapper.Map<IEnumerable<GetTicketsListResponse>>(tickets);

            return response;
        }

        public async Task Remove(long ticketId)
        {
            var ticket = await _unitOfWork.TicketsRepository.FindByIdAsync(ticketId);
            if (ticket == null) throw new NotFoundException("Ticket not found");

            _unitOfWork.TicketsRepository.Remove(ticket);
            await _unitOfWork.SaveAsync();
        }
        public async Task Close(long ticketId)
        {
            var ticket = await _unitOfWork.TicketsRepository.GetByConditionAsync(t => t.Id == ticketId);
            if (ticket == null) throw new NotFoundException("Ticket not found");

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

            _unitOfWork.TicketsRepository.Update(ticket);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAfterSendMessage(Ticket ticket, string creatorRole)
        {
            if (creatorRole == "User" && ticket.Status != TicketStatus.NotChecked)
            {
                ticket.Status = TicketStatus.NotChecked;
                _unitOfWork.TicketsRepository.Update(ticket);
                await _unitOfWork.SaveAsync();
            }
            else if (creatorRole == "Admin" && ticket.Status != TicketStatus.Checked)
            {
                if (ticket.FirstResponseDate == null) ticket.FirstResponseDate = DateTime.Now;
                ticket.Status = TicketStatus.Checked;
                _unitOfWork.TicketsRepository.Update(ticket);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
