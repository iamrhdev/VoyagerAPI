using Voyager.Domain.Entities.Common;

namespace Voyager.Domain.Entities
{
    public class Hotel : BaseEntity
    {
        public string HotelName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public float Rating { get; set; } = 0;
        public int ReviewCount { get; set; } = 0;
    }
}
