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
using Microsoft.OpenApi.Models;
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
        public UserManager<ApplicationUser> UserManager { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "ESTACIONAMENTO",
                        Version = "API V1",
                        Description = "API REST criada com o ASP.NET Core 6.0 para controle de entrada e saída de veículos",
                        Contact = new OpenApiContact
                        {
                            Name = "Daniel Haro",
                            Url = new Uri("https://github.com/danielmorine")
                        }
                    });

            });

            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

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

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");

            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });         
            
        }
    }
}
