using Infrastructure.Schemas;

namespace ParkingWeb.services.Interfaces
{
    public interface ITokenService
    {
        string GetToken(ApplicationUser user);
    }
}
