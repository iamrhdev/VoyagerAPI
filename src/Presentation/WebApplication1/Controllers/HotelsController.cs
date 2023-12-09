using Microsoft.AspNetCore.Mvc;
using Voyager.Application.Abstraction.Services;
using Voyager.Application.DTOs.Hotel_DTOs;
using Voyager.Application.Wrappers;

namespace Voyager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateHotel(HotelCreateDto hotelCreateDto)
        {
            await _hotelService.CreateAsync(hotelCreateDto);
            return Ok();
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(int page = 1,
                                                int pageSize = 3)
        {
            PaginatedResult<HotelGetDto> response = await _hotelService.GetHotelsPaginated(page,pageSize);
            return Ok(response);
        }
    }
}
