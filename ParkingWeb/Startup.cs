using Infrastructure;
using Infrastructure.Schemas;
using Infrastructure.Script;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParkingWeb.Extensions;
using ParkingWeb.Extensions.IOC;
using System;
using System.Linq;

namespace ParkingWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public UserManager<ApplicationUser> UserManager {get;set;}

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("test"));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.RepositoryIOC().ServiceIOC().TokenConfiguration(this.Configuration);
            services.AddMvc();
            services.AddHttpContextAccessor();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.Configure<PasswordHasherOptions>(options =>
                options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            

            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (context.Database.EnsureCreated())
            {
                try
                {
                    var roles = RoleScript.Roles();
                    var user = AdministratorScript.ApplicationUser();
                    var userRole = UserRoleScript.UserRole(roles.FirstOrDefault(x => x.NormalizedName.Equals("ADMINISTRATOR")), user);

                    context.Roles.AddRangeAsync(roles).Wait();
                    context.SaveChangesAsync().Wait();

                    context.ApplicationUser.Add(user);

                    //using var scope2 = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
                    //using var service = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                    //service.CreateAsync(user, user.PasswordHash);
                    
                    context.UserRoles.Add(userRole);
                    context.SaveChangesAsync().Wait();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
