using Voyager.Application.DTOs.Hotel_DTOs;
using Voyager.Application.Wrappers;

namespace Voyager.Application.Abstraction.Services
{
    public interface IHotelService
    {
        Task CreateAsync(HotelCreateDto hotelCreateDto);
        Task<PaginatedResult<HotelGetDto>> GetHotelsPaginated(int page = 1, int pageSize = 3);
    }
}
