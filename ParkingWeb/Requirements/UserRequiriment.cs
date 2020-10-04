using Microsoft.Extensions.Configuration;
using ParkingWeb.Configurations;
using ParkingWeb.Enums;

namespace ParkingWeb.Requirements
{
    public class UserRequiriment : BaseRequirement
    {
        public UserRequiriment(TokenConfiguration tokenConfiguration, SigningConfiguration signingConfiguration, IConfiguration configuration) : base(tokenConfiguration, signingConfiguration, configuration) { }
        protected override PolicyType PolicyType
        {
            get
            {
                return PolicyType.USER;
            }
        }
    }
}
