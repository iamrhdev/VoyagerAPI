using Voyager.Application.Abstraction.Repositories.IVisitorRepositories;
using Voyager.Domain.Entities;
using Voyager.Persistence.Contexts;

namespace Voyager.Persistence.Implementations.Repositories.VisitorRepositories
{
    public class VisitorReadRepository : ReadRepository<Visitor>, IVisitorReadRepository
    {
        public VisitorReadRepository(VoyagerDbContext context) : base(context) { }
    }
}
