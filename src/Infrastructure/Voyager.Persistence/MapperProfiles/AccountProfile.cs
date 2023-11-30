using AutoMapper;
using Voyager.Application.DTOs.Auth_DTOs;
using Voyager.Domain.Identity;

namespace Voyager.Persistence.MapperProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<AppUser, UserRegisterDto>().ReverseMap();
        }
    }
}
