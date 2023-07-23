using Domain.Common;

namespace Application.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        public void Add(T entity);
        public IEnumerable<T> GetAll();

    }
}
