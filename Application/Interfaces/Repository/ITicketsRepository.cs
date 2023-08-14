using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace Application.Interfaces.Repository
{
    public interface ITicketsRepository : IBaseRepository<Ticket>
    {
        public Task<Ticket?> FindByIdAsync(long id);
        public Task RemoveAsync(Ticket ticket);
        public Task UpdateAsync(Ticket ticket);

    }
}
