using ParkingWeb.Models.Login;
using System.Threading.Tasks;

namespace ParkingWeb.services.Interfaces
{
    public interface ILoginService
    {
        Task LoginAsync(LoginModel model);
    }
}
