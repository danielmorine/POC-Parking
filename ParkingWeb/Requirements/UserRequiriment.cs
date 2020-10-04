using Microsoft.Extensions.Configuration;
using ParkingWeb.Enums;

namespace ParkingWeb.Requirements
{
    public class UserRequiriment : BaseRequirement
    {
        public UserRequiriment(IConfiguration configuration) : base(configuration) { }
        protected override PolicyType PolicyType
        {
            get
            {
                return PolicyType.USER;
            }
        }
    }
}
