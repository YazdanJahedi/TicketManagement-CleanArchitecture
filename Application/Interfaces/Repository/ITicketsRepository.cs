using Domain.Entities;

namespace Application.Interfaces.Repository
{
    public interface ITicketsRepository : IBaseRepository<Ticket>
    {
        public Task<Ticket?> FindByIdAsync(long id);
        public void Update(Ticket ticket);
        public void Remove(Ticket ticket);

    }
}
