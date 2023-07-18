using Application.Features.CreateResponse;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Bson;

namespace Application.Repository
{
    public interface IResponsesRepository : IBaseRepository<Response>
    {
        public bool IsContextNull();
        public void AddResponseAsync(Response response);
        public IQueryable<Response> FindAllResonsesByTicketId(long ticketId);
        public void RemoveListOfResponses(List<Response> items); //
    }
}
