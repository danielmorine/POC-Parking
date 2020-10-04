using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ParkingWeb.Enums;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ParkingWeb.Requirements
{
    public abstract class BaseRequirement : AuthorizationHandler<BaseRequirement>, IAuthorizationRequirement
    {
        public const string AuthorizationHeader = "Authorization";
        private readonly TokenValidationParameters _tokenValidationParameters;
        protected abstract PolicyType PolicyType { get; }

        public BaseRequirement(IConfiguration configuration)
        {
            _tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.
                ASCII.GetBytes(configuration["TokenConfiguration:Secret"])),
                ValidAudience = configuration["TokenConfiguration:Audience"],
                ValidIssuer = configuration["TokenConfiguration:Issuer"]
            };
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, BaseRequirement requirement)
        {
            await Task.Run(() =>
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    if (!context.User.HasClaim(x => x.Value.Equals(this.PolicyType.ToString())))
                    {                        
                        context.Fail();
                        return;
                    }

                    context.Succeed(requirement);
                }
            });
        }        
    }
    public class CustomHeader
    {
        public string [] Authorization { get; set; }
    }
}
