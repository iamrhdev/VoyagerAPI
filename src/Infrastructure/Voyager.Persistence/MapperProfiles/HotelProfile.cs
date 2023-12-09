using AutoMapper;
using Voyager.Application.DTOs.Hotel_DTOs;
using Voyager.Domain.Entities;

namespace Voyager.Persistence.MapperProfiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<Hotel, HotelCreateDto>().ReverseMap();
            CreateMap<Hotel,HotelGetDto>().ReverseMap();
        }
    }
}
