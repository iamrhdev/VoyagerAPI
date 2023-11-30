using AutoMapper;
using Voyager.Application.DTOs.Auth_DTOs;
using Voyager.Domain.Entities;

namespace Voyager.Persistence.MapperProfiles
{
    public class HotelManagerProfile : Profile
    {
        public HotelManagerProfile()
        {
            CreateMap<HotelManager, UserRegisterDto>().ReverseMap(); 
        }
    }
}
