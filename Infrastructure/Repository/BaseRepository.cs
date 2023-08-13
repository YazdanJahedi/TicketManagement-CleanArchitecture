using Application.Repository;
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

        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? condition = null, params string[] includes)
        {
            var query = ApplyIncludes(_context.Set<T>(), includes);

            if (condition != null) query = query.Where(condition);
           
            return await query.ToListAsync();
        }
        public async Task<T?> GetAsync(Expression<Func<T, bool>> condition, params string[] includes)
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
