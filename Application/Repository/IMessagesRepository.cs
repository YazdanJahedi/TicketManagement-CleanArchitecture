using Application.Features.CreateResponse;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Bson;

namespace Application.Repository
{
    public interface IMessagesRepository : IBaseRepository<Message>
    {
        public void AddResponseAsync(Message response);
        public IQueryable<Message> FindAllByTicketId(long ticketId);
        public void RemoveAllByTicketId(long ticketId);
    }
}
