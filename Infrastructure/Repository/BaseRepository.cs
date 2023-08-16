using Application.Interfaces.Repository;
using Domain.Common;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync(int number = int.MaxValue, Expression<Func<T, bool>>? condition = null, params string[] includes)
        {
            IQueryable<T> context = _context.Set<T>().OrderByDescending(e => e.CreationDate);
            
            if (number !=  int.MaxValue) context = context.Take(number);

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
