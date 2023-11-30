using Microsoft.Extensions.DependencyInjection;
using Voyager.Application.Abstraction.Services;
using Voyager.Infrastructure.Services;

namespace Voyager.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}
