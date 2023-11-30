using AutoMapper;
using Voyager.Application.DTOs.Auth_DTOs;
using Voyager.Domain.Entities;

namespace Voyager.Persistence.MapperProfiles
{
    public class VisitorProfile : Profile
    {
        public VisitorProfile()
        {
            CreateMap<Visitor, UserRegisterDto>().ReverseMap();
        }
    }
}
