using Voyager.Application.Abstraction.Repositories.IHotelManagerRepositories;
using Voyager.Application.Abstraction.Repositories.IHotelRepositories;
using Voyager.Domain.Entities;
using Voyager.Persistence.Contexts;

namespace Voyager.Persistence.Implementations.Repositories.HotelRepositories
{
    public class HotelWriteRepository : WriteRepository<Hotel>, IHotelWriteRepository
    {
        public HotelWriteRepository(VoyagerDbContext context) : base(context) { }
    }
}
