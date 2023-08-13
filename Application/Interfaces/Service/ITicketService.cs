using Application.Dtos.TicketDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface ITicketService
    {
        public Task Add(CreateTicketRequest request);
        public Task Remove(long ticketId);
        public Task<GetTicketResponse> Get(long ticketId);
        public Task<IEnumerable<GetTicketsListResponse>> GetAll(int number);
        public Task<IEnumerable<GetTicketsListResponse>> GetAllByUser(string username);     
        public Task Close(long ticketId);
        public Task UpdateTicketAfterSendNewMessage(Ticket ticket, string creatorRole);
    }
}
