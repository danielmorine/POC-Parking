using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ParkingWeb.Configurations;
using ParkingWeb.Requirements;
using System;

namespace ParkingWeb.Extensions
{
    public static class TokenExtension
    {      
        public static IServiceCollection TokenConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var signingCredentials = new SigningConfiguration();

            services.AddSingleton(signingCredentials);

            var tokenConfiguration = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(configuration).Configure(tokenConfiguration);

            services.AddSingleton(tokenConfiguration);

            services.AddAuthentication(authConfig =>
            {
                authConfig.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authConfig.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearerOpt => 
            {
                var paramsValidation = bearerOpt.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingCredentials.Key;
                paramsValidation.ValidAudience = tokenConfiguration.Audience;
                paramsValidation.ValidIssuer = tokenConfiguration.Issuer;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });


            services.AddAuthorization(auth =>
            {
                var bearerRequirement = new BearerRequirement(tokenConfiguration, signingCredentials);
                var administratorRequirement = new AdministratorRequirement(tokenConfiguration, signingCredentials);
                var userRequirement = new UserRequiriment(tokenConfiguration, signingCredentials);

                var bearerAuthorization = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .AddRequirements(bearerRequirement)
                .RequireAuthenticatedUser().Build();

                var administratorBearer = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .AddRequirements(administratorRequirement)
                .RequireAuthenticatedUser().Build();

                var userBearer = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .AddRequirements(userRequirement)
                .RequireAuthenticatedUser().Build();

                auth.AddPolicy("Bearer", bearerAuthorization);
                auth.AddPolicy("AdministratorBearer", administratorBearer);
                auth.AddPolicy("UserBearer", userBearer);

            });



            return services;
        }
    }  
}
