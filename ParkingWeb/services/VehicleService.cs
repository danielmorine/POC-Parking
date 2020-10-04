using Infrastructure.Helpers;
using Infrastructure.Queries.Vehicle;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Schemas;
using ParkingWeb.Exceptions;
using ParkingWeb.Models.Vehicle;
using ParkingWeb.services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ParkingWeb.services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUserCompanyRepository _userCompanyRepository;
        private readonly IParkingRepository _parkingRepository;

        public VehicleService(IVehicleRepository vehicleRepository, IUserCompanyRepository userCompanyRepository, IParkingRepository parkingRepository)
        {
            _vehicleRepository = vehicleRepository;
            _userCompanyRepository = userCompanyRepository;
            _parkingRepository = parkingRepository;
        }

        public async Task UpdateAsync(VehicleUpdateModel model, Guid userID)
        {
            await ValidateUpdate(model, userID);

            var vehicle = await _vehicleRepository.FirstOrDefaultAsync(x => x.Plate.Equals(model.Plate));

            vehicle.Color = string.IsNullOrEmpty(model.Color) ? vehicle.Color : model.Color;
            vehicle.Make = string.IsNullOrEmpty(model.Make) ? vehicle.Make : model.Make;
            vehicle.Model = string.IsNullOrEmpty(model.Model) ? vehicle.Model : model.Model;

            await _vehicleRepository.SaveChangeAsync();
        }

        public async Task DeleteAsync(string plate, Guid userID)
        {
            await ValidatePlate(plate, userID);

            var vehicle = await _vehicleRepository.FirstOrDefaultAsync(x => x.Plate.Equals(plate));

            if (await _parkingRepository.AnyAsync(x => x.VehicleID == vehicle.ID))
            {
                throw new CustomExceptions("Não é possível remover este veículo, o mesmo já deu entrada no estaciomento");
            }

            _vehicleRepository.Delete(vehicle);

            await _vehicleRepository.SaveChangeAsync();
        }

        public async Task AddAsync(VehicleModel model, Guid userID)
        {
            Validate(model);
           
            var company = await _userCompanyRepository.FirstOrDefaultAsync(x => x.UserID.Equals(userID.ToString()));

            if (await _vehicleRepository.AnyAsync(x => x.CompanyID == company.CompanyID && x.Plate.Equals(model.Plate)))
            {
                throw new CustomExceptions("Placa já cadastrada");
            }

            await _vehicleRepository.AddAsync(new Vehicle 
            { 
                Color = model.Color,
                CompanyID = company.CompanyID,
                CreatedDate = DateTimeOffset.UtcNow,
                ID = Guid.NewGuid(),
                Make = model.Make,
                Plate = model.Plate,
                TypeID = model.TypeID,
                Model = model.Model,                
            });

            await _vehicleRepository.SaveChangeAsync();
        }        
        public async Task<IEnumerable<VehicleQuery>> GetAllAsync(Guid userID)
        {           
            var company = await _userCompanyRepository.FirstOrDefaultAsync(x => x.UserID.Equals(userID.ToString()));

            if (company == null)
            {
                throw new CustomExceptions("Não foi possível identificar a empresa");
            }

            Expression<Func<Vehicle, bool>> expression = x => x.CompanyID == company.CompanyID;
            Expression<Func<Vehicle, VehicleQuery>> selection = x => new VehicleQuery
            {
                Color = x.Color,
                Make = x.Make,
                Model = x.Model,
                Plate = x.Plate,
            };

            var result = await _vehicleRepository.GetAllAsync(expression, selection, false, int.MaxValue, 1);

            return result;
        }
        public async Task<VehicleQuery> GetByPlateAsync(string plate, Guid userID)
        {
            var vehicle = await _vehicleRepository.FirstOrDefaultAsync(x => x.Plate.Equals(plate), false);
            if (vehicle == null)
            {
                throw new CustomExceptions("Veículo não encontrado");
            }
            return new VehicleQuery
            {
                Color = vehicle.Color,
                Make = vehicle.Make,
                Model = vehicle.Model,
                Plate = vehicle.Plate,
            };
        }       
        private void Validate(VehicleModel model)
        {
            if (model == null)
            {
                throw new CustomExceptions("É necessário preencher todos os campos");
            }

            StringHelper.StringNullOrEmpty(model.Color);
            StringHelper.StringNullOrEmpty(model.Make);
            StringHelper.StringNullOrEmpty(model.Model);
            StringHelper.StringNullOrEmpty(model.Plate);
        }
    
        private async Task ValidateUpdate(VehicleUpdateModel model, Guid userID)
        {
            if (model == null)
            {
                throw new CustomExceptions("Objeto não identificado");
            }

            await ValidatePlate(model.Plate, userID);
        }

        private async Task ValidatePlate(string plate, Guid userID)
        {
            if (string.IsNullOrEmpty(plate))
            {
                throw new CustomExceptions("Sem placa");
            }
            else if (plate.Length != 7)
            {
                throw new CustomExceptions("Formato invalido de placa, deve ter no máximo 7 caracteres");
            }

            var company = await _userCompanyRepository.FirstOrDefaultAsync(x => x.UserID.Equals(userID.ToString()));

            if (company == null)
            {
                throw new CustomExceptions("Estacionamento não encontrado");
            } else if (!await _vehicleRepository.AnyAsync(x => x.CompanyID == company.CompanyID && x.Plate.Equals(plate)))
            {
                throw new CustomExceptions("A placa informada não pertence ao estacionamento");
            }
        }
    }
}
