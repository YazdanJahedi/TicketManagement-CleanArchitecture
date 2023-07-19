using Domain.Common;

namespace Application.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        public void CheckNull();
        public void Add(T entity);
        public Task<IEnumerable<T>> GetAllAsync();

    }
}
