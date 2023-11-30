using Microsoft.AspNetCore.Identity;

namespace Voyager.Domain.Identity
{
    public class AppUser : IdentityUser 
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
    }
}
