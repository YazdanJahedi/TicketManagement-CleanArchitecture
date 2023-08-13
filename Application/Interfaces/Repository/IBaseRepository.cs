using Domain.Common;
using System.Linq.Expressions;

namespace Application.Interfaces.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task<T?> GetAsync(Expression<Func<T, bool>> condition, params string[] includes);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? condition = null, params string[] includes);
        Task SaveChangesAsync();
    }
}
