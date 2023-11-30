using Voyager.Application.Abstraction.Repositories.IHotelManagerRepositories;
using Voyager.Domain.Entities;
using Voyager.Persistence.Contexts;

namespace Voyager.Persistence.Implementations.Repositories.HotelManagerRepositories
{
    public class HotelManagerReadRepository : ReadRepository<HotelManager>, IHotelManagerReadRepository
    {
        public HotelManagerReadRepository(VoyagerDbContext context) : base(context)
        {
        }
    }
}
