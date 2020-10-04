using ParkingWeb.Models.Register;
using System.Threading.Tasks;

namespace ParkingWeb.services.Interfaces
{
    public interface IRegisterService
    {
        Task AddAsync(RegisterModel model);
    }
}
