using Voyager.Application.Abstraction.Repositories.IHotelRepositories;
using Voyager.Domain.Entities;
using Voyager.Persistence.Contexts;

namespace Voyager.Persistence.Implementations.Repositories.HotelRepositories
{
    public class HotelReadRepository : ReadRepository<Hotel>, IHotelReadRepository
    {
        public HotelReadRepository(VoyagerDbContext context) : base(context) { }
    }
}
