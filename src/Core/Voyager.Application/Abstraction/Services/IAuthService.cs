using Voyager.Application.DTOs.Auth_DTOs;
using Voyager.Application.DTOs.Response_DTOs;

namespace Voyager.Application.Abstraction.Services
{
    public interface IAuthService
    {
        Task Register(UserRegisterDto userRegisterDto);
        Task<TokenResponseDto> Login(UserSignInDto userSignInDto);
        Task<bool> UserNameExists(string userName);
    }
}
