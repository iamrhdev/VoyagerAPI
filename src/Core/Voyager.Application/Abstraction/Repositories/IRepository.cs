using Microsoft.EntityFrameworkCore;
using Voyager.Domain.Entities.Common;

namespace Voyager.Application.Abstraction.Repositories
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        public DbSet<T> Table { get; }
    }
}
