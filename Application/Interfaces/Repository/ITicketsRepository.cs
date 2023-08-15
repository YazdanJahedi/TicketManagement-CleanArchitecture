using Domain.Entities;

namespace Application.Interfaces.Repository
{
    public interface ITicketsRepository : IBaseRepository<Ticket>
    {
        public Task<Ticket?> FindByIdAsync(long id);
        public Task RemoveAsync(Ticket ticket);
        public Task UpdateAsync(Ticket ticket);

    }
}
