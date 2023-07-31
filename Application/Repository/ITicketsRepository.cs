using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace Application.Repository
{
    public interface ITicketsRepository : IBaseRepository<Ticket>
    {
        public IEnumerable<Ticket> FindAllByCreatorId(long creatorId);
        public Task<Ticket?> FindByIdAsync(long id);
        public Task RemoveAsync(Ticket ticket);
    }
}
