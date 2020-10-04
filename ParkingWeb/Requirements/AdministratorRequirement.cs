using Microsoft.Extensions.Configuration;
using ParkingWeb.Enums;

namespace ParkingWeb.Requirements
{
    public class AdministratorRequirement : BaseRequirement
    {
        public AdministratorRequirement(IConfiguration configuration)
            : base(configuration) { }

        protected override PolicyType PolicyType
        {
            get
            {
                return PolicyType.ADMINISTRATOR;
            }
        }
    }
}
