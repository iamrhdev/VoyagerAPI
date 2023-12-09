using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Voyager.Application.Abstraction.Repositories;
using Voyager.Application.Abstraction.Repositories.IHotelManagerRepositories;
using Voyager.Application.Abstraction.Repositories.IHotelRepositories;
using Voyager.Application.Abstraction.Repositories.IVisitorRepositories;
using Voyager.Application.Abstraction.Services;
using Voyager.Application.Validators.AuthValidators;
using Voyager.Domain.Identity;
using Voyager.Persistence.Contexts;
using Voyager.Persistence.Implementations.Repositories;
using Voyager.Persistence.Implementations.Repositories.HotelManagerRepositories;
using Voyager.Persistence.Implementations.Repositories.HotelRepositories;
using Voyager.Persistence.Implementations.Repositories.VisitorRepositories;
using Voyager.Persistence.Implementations.Services;
using Voyager.Persistence.MapperProfiles;

namespace Voyager.Persistence.ExtensionMethods
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<VoyagerDbContextInitialiser>();
            services.AddDbContext<VoyagerDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
            AddReadRepositories(services);
            AddWriteRepositories(services);
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IHotelService, HotelService>();

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<UserRegisterDtoValidator>();
            services.AddAutoMapper(typeof(AccountProfile).Assembly);
        }

        public static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(identityOptions =>
            {
                identityOptions.User.RequireUniqueEmail = true;
                identityOptions.Password.RequiredLength = 8;
                identityOptions.Lockout.MaxFailedAccessAttempts = 5;
            }).AddDefaultTokenProviders()
              .AddEntityFrameworkStores<VoyagerDbContext>();
        }
        private static void AddReadRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped<IVisitorReadRepository, VisitorReadRepository>();
            services.AddScoped<IHotelManagerReadRepository, HotelManagerReadRepository>();
            services.AddScoped<IHotelReadRepository, HotelReadRepository>();
        }
        private static void AddWriteRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped<IVisitorWriteRepository, VisitorWriteRepository>();
            services.AddScoped<IHotelManagerWriteRepository, HotelManagerWriteRepository>();
            services.AddScoped<IHotelWriteRepository, HotelWriteRepository>();
        }
    }
}
