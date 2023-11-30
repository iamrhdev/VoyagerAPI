using Voyager.Application.DTOs.Response_DTOs;
using Voyager.Domain.Identity;

namespace Voyager.Application.Abstraction.Services
{
    public interface IJwtService
    {
        Task<TokenResponseDto> CreateJwt(AppUser user);
    }
}
