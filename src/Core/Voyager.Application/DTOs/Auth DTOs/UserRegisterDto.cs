using Voyager.Domain.Enums;

namespace Voyager.Application.DTOs.Auth_DTOs
{
    public record UserRegisterDto(string UserName, string FirstName, string LastName, string Email, string Password, string PasswordConfirm, string PhoneNumber, Roles Role);
}
