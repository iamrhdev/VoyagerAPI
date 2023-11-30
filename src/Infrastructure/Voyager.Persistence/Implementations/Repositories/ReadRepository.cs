using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using Voyager.Application.Abstraction.Repositories;
using Voyager.Domain.Entities.Common;
using Voyager.Persistence.Contexts;

namespace Voyager.Persistence.Implementations.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
    {
        private readonly VoyagerDbContext _context;
        public DbSet<T> Table => _context.Set<T>();

        public ReadRepository(VoyagerDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll(bool isTracking = true, params string[] includes)
        {
            var query = Table.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return isTracking ? query : query.AsNoTracking();
        }

        public IQueryable<T> GetAllByExpression(Expression<Func<T, bool>> expression,
                                                int take,
                                                int skip,
                                                bool isTracking = true,
                                                params string[] includes)
        {
            var query = Table.Where(expression).Skip(skip).Take(take).AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return isTracking ? query : query.AsNoTracking();
        }

        public IQueryable<T> GetAllByExpressionOrderBy(Expression<Func<T, bool>> expression,
                                                       int take,
                                                       int skip,
                                                       Expression<Func<T, object>> expressionOrder,
                                                       bool isOrdered = true,
                                                       bool isTracking = true,
                                                       params string[] includes)
        {
            var query = Table.Where(expression).AsQueryable();
            query = isOrdered ? query.OrderBy(expressionOrder) : query.OrderByDescending(expression);
            query = query.Skip(skip).Take(take);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return isTracking ? query : query.AsNoTracking();
        }

        public async Task<T?> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool isTracking = true)
        {
            var query = isTracking ? Table : Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(expression);
        }
        public async Task<T?> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool isTracking = true, params string[] includes)
        {
            var query = isTracking ? Table : Table.AsNoTracking();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.SingleOrDefaultAsync(expression);
        }
        public async Task<T?> GetByIdAsync(Guid id) => await Table.FindAsync(id);

    }
}
