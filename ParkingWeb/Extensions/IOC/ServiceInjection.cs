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
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IVehicleService, VehicleService>();
            return services;
        }
    }
}
