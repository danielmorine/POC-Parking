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
using Microsoft.Net.Http.Headers;
using ParkingWeb.Extensions;
using ParkingWeb.Extensions.IOC;
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
            services.AddMvc(options =>
            {
                options.FormatterMappings.SetMediaTypeMappingForFormat
                    ("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat
                    ("config", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat
                    ("js", MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters();

            services.AddHttpContextAccessor();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });           
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
                    var roles = RoleScript.Roles();
                    var user = AdministratorScript.ApplicationUser();
                    var userRole = UserRoleScript.UserRole(roles.FirstOrDefault(x => x.NormalizedName.Equals("ADMINISTRATOR")), user);
                    var types = TypeScript.GetTypes();

                    context.Roles.AddRangeAsync(roles).Wait();
                    context.SaveChangesAsync().Wait();

                    context.ApplicationUser.Add(user);
                                     
                    context.UserRoles.Add(userRole);
                    context.SaveChangesAsync().Wait();

                    context.Type.AddRangeAsync(types).Wait();                
            }
        }
    }
}
