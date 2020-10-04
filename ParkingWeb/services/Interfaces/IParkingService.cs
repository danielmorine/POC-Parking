using Infrastructure.Queries.Park;
using ParkingWeb.Models.Parking;
using System;
using System.Threading.Tasks;

namespace ParkingWeb.services.Interfaces
{
    public interface IParkingService
    {
        Task StartAsync(Guid userID, ParkingModel model);
        Task EndAsync(Guid userID, ParkingModel model);
        Task<ParkQuery> GetTotalsAsync(Guid userID);
    }
}
