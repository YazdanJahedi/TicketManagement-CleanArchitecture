using Domain.Common;

namespace Application.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        public Task AddAsync(T entity); 
        public Task<IEnumerable<T>> GetAllAsync();

    }
}
