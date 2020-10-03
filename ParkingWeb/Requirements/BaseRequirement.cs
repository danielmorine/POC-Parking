using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ParkingWeb.Configurations;
using ParkingWeb.Enums;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace ParkingWeb.Requirements
{
    public abstract class BaseRequirement : AuthorizationHandler<BaseRequirement>, IAuthorizationRequirement
    {
        public const string AuthorizationHeader = "Authorization";
        private readonly TokenValidationParameters _tokenValidationParameters;
        protected abstract PolicyType PolicyType { get; }

        public BaseRequirement(TokenConfiguration tokenconfiguration, SigningConfiguration signingConfigurations)
        {
            _tokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = tokenconfiguration.Issuer,
                ValidAudience = tokenconfiguration.Audience,
                IssuerSigningKey = signingConfigurations.Key,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, BaseRequirement requirement)
        {
            await Task.Run(() =>
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    if (!context.User.HasClaim(x => x.Value.Equals(this.PolicyType.ToString())) || !TokenIsValid(context))
                    {
                        context.Fail();
                        return;
                    }

                    context.Succeed(requirement);
                }
            });
        }

        protected bool TokenIsValid(AuthorizationHandlerContext context)
        {
            var token = GetToken(context);
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = null;

            try
            {
                tokenHandler.ValidateToken(token, _tokenValidationParameters, out securityToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GetToken(AuthorizationHandlerContext context)
        {
            var @dynamic = (dynamic)context.Resource;
            @dynamic = @dynamic.HttpContext.Request.headers;

            var json = JsonConvert.SerializeObject(dynamic);
            CustomHeader headers = JsonConvert.DeserializeObject<CustomHeader>(json);

            var token = headers.Authorization[0];
            if (string.IsNullOrEmpty(token?.Trim()))
            {
                return string.Empty;
            }

            return token.Replace(JwtBearerDefaults.AuthenticationScheme, string.Empty).Trim();
        }
    }
    public class CustomHeader
    {
        public string [] Authorization { get; set; }
    }
}
