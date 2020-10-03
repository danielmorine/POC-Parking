using Infrastructure.Schemas;
using ParkingWeb.Enums;

namespace ParkingWeb.services.Interfaces
{
    public interface ITokenService
    {
        string GetToken(ApplicationUser user, PolicyType policyType);
    }
}
