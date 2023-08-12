using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITicketService
    {
        public Task UpdateTicketAfterSendNewMessage(Ticket ticket, string creatorRole);
    }
}
