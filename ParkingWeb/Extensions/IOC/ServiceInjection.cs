using Microsoft.Extensions.DependencyInjection;
using ParkingWeb.services;
using ParkingWeb.services.Interfaces;

namespace ParkingWeb.Extensions.IOC
{
    public static class ServiceInjection
    {
        public static IServiceCollection ServiceIOC(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
