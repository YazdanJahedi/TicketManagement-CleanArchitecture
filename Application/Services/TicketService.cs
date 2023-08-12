using Application.Interfaces;
using Application.Repository;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketsRepository _ticketsRepository;

        public TicketService(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
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
