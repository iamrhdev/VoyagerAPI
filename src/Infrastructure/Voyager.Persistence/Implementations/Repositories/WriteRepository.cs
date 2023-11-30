using Microsoft.EntityFrameworkCore;
using Voyager.Application.Abstraction.Repositories;
using Voyager.Domain.Entities.Common;
using Voyager.Persistence.Contexts;

namespace Voyager.Persistence.Implementations.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity, new()
    {
        private readonly VoyagerDbContext _context;
        public DbSet<T> Table => _context.Set<T>();


        public WriteRepository(VoyagerDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity) => await Table.AddAsync(entity);

        public async Task AddRangeAsync(ICollection<T> entities) => await Table.AddRangeAsync(entities);

        public void Remove(T entity) => Table.Remove(entity);

        public void RemoveRange(ICollection<T> entities) => Table.RemoveRange(entities);

        public async Task SaveChangeAsync() => await _context.SaveChangesAsync();

        public void Update(T entity) => Table.Update(entity);
    }
}
