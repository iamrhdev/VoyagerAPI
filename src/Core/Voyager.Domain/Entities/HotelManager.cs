using Voyager.Domain.Entities.Common;
using Voyager.Domain.Identity;

namespace Voyager.Domain.Entities
{
    public class HotelManager : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
    }
}
