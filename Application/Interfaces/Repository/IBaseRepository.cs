using Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Application.Interfaces.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T?> GetByConditionAsync(Expression<Func<T, bool>> condition, params string[] includes);
        Task<IEnumerable<T>> GetAllAsync(int first = 0, int last = int.MaxValue, Expression<Func<T, bool>>? condition = null, params string[] includes);
        Task AddAsync(T entity);
    }
}
