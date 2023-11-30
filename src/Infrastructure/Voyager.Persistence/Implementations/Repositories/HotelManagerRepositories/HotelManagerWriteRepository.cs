using Voyager.Application.Abstraction.Repositories.IHotelManagerRepositories;
using Voyager.Domain.Entities;
using Voyager.Persistence.Contexts;

namespace Voyager.Persistence.Implementations.Repositories.HotelManagerRepositories
{
    public class HotelManagerWriteRepository : WriteRepository<HotelManager>, IHotelManagerWriteRepository
    {
        public HotelManagerWriteRepository(VoyagerDbContext context) : base(context)
        {
        }
    }
}
