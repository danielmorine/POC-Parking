using ParkingWeb.Configurations;
using ParkingWeb.Enums;

namespace ParkingWeb.Requirements
{
    public class AdministratorRequirement : BaseRequirement
    {
        public AdministratorRequirement(TokenConfiguration tokenconfiguration, SigningConfiguration signingConfigurations)
            : base(tokenconfiguration, signingConfigurations) { }

        protected override PolicyType PolicyType
        {
            get
            {
                return PolicyType.ADMINISTRATOR;
            }
        }
    }
}
