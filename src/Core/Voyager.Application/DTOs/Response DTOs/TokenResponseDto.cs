namespace Voyager.Application.DTOs.Response_DTOs
{
    public record TokenResponseDto(string Jwt, DateTime JwtExpiration, string RefreshToken, DateTime RefreshTokenExpiration);
}
