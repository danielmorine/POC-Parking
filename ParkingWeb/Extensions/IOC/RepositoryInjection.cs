using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ParkingWeb.Extensions.IOC
{
    public static class RepositoryInjection
    {
        public static IServiceCollection RepositoryIOC(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}
