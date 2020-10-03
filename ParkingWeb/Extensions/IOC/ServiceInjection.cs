using Microsoft.Extensions.DependencyInjection;

namespace ParkingWeb.Extensions.IOC
{
    public static class ServiceInjection
    {
        public static IServiceCollection ServiceIOC(this IServiceCollection services)
        {
            return services;
        }
    }
}
