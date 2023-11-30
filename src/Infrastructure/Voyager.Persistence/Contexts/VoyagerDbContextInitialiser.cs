using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Voyager.Domain.Enums;
using Voyager.Domain.Identity;

namespace Voyager.Persistence.Contexts
{
    public class VoyagerDbContextInitialiser
    {
        private readonly VoyagerDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public VoyagerDbContextInitialiser(VoyagerDbContext context,
                                           RoleManager<IdentityRole> roleManager,
                                           UserManager<AppUser> userManager,
                                           IConfiguration configuration)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task InitialiseAsync()
        {
            await _context.Database.MigrateAsync();
        }
        public async Task RoleSeedAsync()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }
        public async Task UserSeedAsync()
        {
            AppUser masterProfile = new()
            {
                UserName = _configuration["Master:UserName"],
                Email = _configuration["Master:Email"],
                PhoneNumber = _configuration["Master:PhoneNumber"]
            };
            await _userManager.CreateAsync(masterProfile, _configuration["Master:Password"]);
            await _userManager.AddToRoleAsync(masterProfile, Roles.Master.ToString());
        }
    }
}
