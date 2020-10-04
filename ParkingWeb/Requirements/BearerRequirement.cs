using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using ParkingWeb.Enums;
using System.Threading.Tasks;

namespace ParkingWeb.Requirements
{
    public class BearerRequirement : BaseRequirement
    {
        public BearerRequirement(IConfiguration configuration) : base(configuration) { }

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
}
