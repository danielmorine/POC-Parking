using Infrastructure.Extensions.FluentAPI;
using Infrastructure.Interfaces;
using Infrastructure.Schemas;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Company> Company {get; set; }
        public DbSet<Parking> Parking {get; set; }
        public DbSet<Type> Type {get; set; }
        public DbSet<UserCompany> UserCompany {get; set; }
        public DbSet<Vehicle> Vehicles {get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {            
            builder.CompanyModelBuilder()
                .ApplicationUserBuilder()
                .ParkingModelBuilder()
                .TypeModelBuilder()
                .UserCompanyModelBuilder()
                .VehicleModelBuilder();

            base.OnModelCreating(builder);
        }
    }
}
