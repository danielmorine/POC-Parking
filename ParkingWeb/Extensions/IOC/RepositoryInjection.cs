using Infrastructure;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ParkingWeb.Extensions.IOC
{
    public static class RepositoryInjection
    {
        public static IServiceCollection RepositoryIOC(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IParkingRepository, ParkingRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<IUserCompanyRepository, UserCompanyRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IUnitOfWork, Infrastructure.UnitOfWork>();

            return services;
        }
    }
}
