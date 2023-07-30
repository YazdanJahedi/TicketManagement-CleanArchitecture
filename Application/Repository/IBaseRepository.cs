using Domain.Common;

namespace Application.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        public Task AddAsync(T entity); // return Task
        public Task<IEnumerable<T>> GetAllAsync();

    }
}
