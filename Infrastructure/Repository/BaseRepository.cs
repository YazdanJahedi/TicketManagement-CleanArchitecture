using Application.Interfaces.Repository;
using Domain.Common;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);   
        }
        public async Task<IEnumerable<T>> GetAllAsync(int first = 0, int last = int.MaxValue, Expression<Func<T, bool>>? condition = null, params string[] includes)
        {
            IQueryable<T> context = _context.Set<T>().OrderByDescending(e => e.CreationDate);

            if (last !=  int.MaxValue) context = context.Take(last);
            if (first != 0) context = context.TakeLast(last - first);

            if (condition != null) context = context.Where(condition);

            var query = ApplyIncludes(context, includes);

            return await query.ToListAsync();
        }
        public async Task<T?> GetByConditionAsync(Expression<Func<T, bool>> condition, params string[] includes)
        {
            var query = ApplyIncludes(_context.Set<T>(), includes);

            return await query.FirstOrDefaultAsync(condition);
        }

        private IQueryable<T> ApplyIncludes(IQueryable<T> query, params string[] includes)
        {
            return includes.Aggregate(query, (current, include) => current.Include(include));
        }

    }
}
