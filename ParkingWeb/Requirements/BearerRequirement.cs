using Microsoft.AspNetCore.Authorization;
using ParkingWeb.Configurations;
using ParkingWeb.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingWeb.Requirements
{
    public class BearerRequirement : BaseRequirement
    {
        public BearerRequirement(TokenConfiguration tokenConfiguration, SigningConfiguration signingConfiguration) : base(tokenConfiguration, signingConfiguration) { }

        protected override PolicyType PolicyType
        {
            get
            {
                return PolicyType.NONE;
            }
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, BaseRequirement requirement)
        {
            await Task.Run(() =>
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    if (!TokenIsValid(context))
                    {
                        context.Fail();
                        return;
                    }

                    context.Succeed(requirement);
                }
            });
        }
    }
}
