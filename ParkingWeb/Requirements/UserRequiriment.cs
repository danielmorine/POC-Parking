using ParkingWeb.Configurations;
using ParkingWeb.Enums;

namespace ParkingWeb.Requirements
{
    public class UserRequiriment : BaseRequirement
    {
        public UserRequiriment(TokenConfiguration tokenConfiguration, SigningConfiguration signingConfiguration) : base(tokenConfiguration, signingConfiguration) { }
        protected override PolicyType PolicyType
        {
            get
            {
                return PolicyType.USER;
            }
        }
    }
}
