using Voyager.Application.Abstraction.Repositories.IVisitorRepositories;
using Voyager.Domain.Entities;
using Voyager.Persistence.Contexts;

namespace Voyager.Persistence.Implementations.Repositories.VisitorRepositories
{
    public class VisitorWriteRepository : WriteRepository<Visitor>, IVisitorWriteRepository
    {
        public VisitorWriteRepository(VoyagerDbContext context) : base(context) { }
    }
}
