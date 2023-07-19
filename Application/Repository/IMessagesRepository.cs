using Application.Features.CreateResponse;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Bson;

namespace Application.Repository
{
    public interface IMessagesRepository : IBaseRepository<Message>
    {
        public IEnumerable<Message> FindAllByTicketId(long ticketId);
        public void RemoveAllByTicketId(long ticketId);
        public Message? FindLastMessageByTicketId(long ticketId);
    }
}
