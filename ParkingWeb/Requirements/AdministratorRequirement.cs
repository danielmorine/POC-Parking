using Microsoft.Extensions.Configuration;
using ParkingWeb.Configurations;
using ParkingWeb.Enums;

namespace ParkingWeb.Requirements
{
    public class AdministratorRequirement : BaseRequirement
    {
        public AdministratorRequirement(TokenConfiguration tokenconfiguration, SigningConfiguration signingConfigurations, IConfiguration configuration)
            : base(tokenconfiguration, signingConfigurations, configuration) { }

        protected override PolicyType PolicyType
        {
            get
            {
                return PolicyType.ADMINISTRATOR;
            }
        }
    }
}
