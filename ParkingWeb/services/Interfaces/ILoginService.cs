using ParkingWeb.Models.Login;
using System.Threading.Tasks;

namespace ParkingWeb.services.Interfaces
{
    public interface ILoginService
    {
        Task<string> LoginAsync(LoginModel model);
    }
}
