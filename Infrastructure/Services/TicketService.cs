using Application.Common.Exceptions;
using Application.Dtos.TicketDtos;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;


namespace Infrastructure.Services
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

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var ticket = new Ticket
                    {
                        CreatorId = claim.Id,
                        Title = request.Title,
                        CreationDate = DateTime.Now,
                        FaqCategoryId = request.FaqCatgoryId,
                        Status = TicketStatus.NotChecked,
                    };
                    await _unitOfWork.TicketsRepository.AddAsync(ticket);
                    await _unitOfWork.SaveAsync();

                    var firstMessage = new Message
                    {
                        TicketId = ticket.Id,
                        CreatorId = claim.Id,
                        CreationDate = DateTime.Now,
                        Text = request.Description,
                    };
                    await _unitOfWork.MessagesRepository.AddAsync(firstMessage);
                    await _unitOfWork.SaveAsync();

                    if (request.Attachments != null)
                        await _messageAttachmentService.UploadRange(request.Attachments, firstMessage.Id);

                    await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
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

        public async Task<IEnumerable<GetTicketsListResponse>> GetAll(int first, int to)
        {
            var claims = _authService.GetClaims();

            var tickets = claims.Role == "Admin" ?
                await _unitOfWork.TicketsRepository.GetAllAsync(first: first, last: to, includes: "Creator") :
                await _unitOfWork.TicketsRepository.GetAllAsync(first: first, last: to, condition: e => e.CreatorId == claims.Id, includes: "Creator");

            var response = _mapper.Map<IEnumerable<GetTicketsListResponse>>(tickets);

            return response;
        }

        public async Task<IEnumerable<GetTicketsListResponse>> GetAllByUser(string username, int first, int to)
        {
            var tickets = await _unitOfWork.TicketsRepository.GetAllAsync(first: first, last: to, condition: t => t.Creator!.Name == username, includes: "Creator");
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

        public void UpdateAfterSendMessage(Ticket ticket, string creatorRole)
        {
            if (creatorRole == "User" && ticket.Status != TicketStatus.NotChecked)
            {
                ticket.Status = TicketStatus.NotChecked;
                _unitOfWork.TicketsRepository.Update(ticket);
            }
            else if (creatorRole == "Admin" && ticket.Status != TicketStatus.Checked)
            {
                if (ticket.FirstResponseDate == null) ticket.FirstResponseDate = DateTime.Now;
                ticket.Status = TicketStatus.Checked;
                _unitOfWork.TicketsRepository.Update(ticket);
            }
        }
    }
}
