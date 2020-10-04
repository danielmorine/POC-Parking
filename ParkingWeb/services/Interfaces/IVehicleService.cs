using Infrastructure.Queries.Vehicle;
using ParkingWeb.Models.Vehicle;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingWeb.services.Interfaces
{
    public interface IVehicleService
    {
        Task AddAsync(VehicleModel model, Guid userID);
        Task<IEnumerable<VehicleQuery>> GetAllAsync(Guid userID);
        Task<VehicleQuery> GetByPlateAsync(string plate, Guid userID);
        Task UpdateAsync(VehicleUpdateModel model, Guid userID);
        Task DeleteAsync(string plate, Guid userID);
    }
}
