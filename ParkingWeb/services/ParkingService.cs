using Infrastructure.Queries.Park;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Schemas;
using ParkingWeb.Exceptions;
using ParkingWeb.Models.Parking;
using ParkingWeb.services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ParkingWeb.services
{
    public class ParkingService : IParkingService
    {
        private readonly IParkingRepository _parkingRepository;
        private readonly IUserCompanyRepository _userCompanyRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public ParkingService(IParkingRepository parkingRepository, 
            IUserCompanyRepository userCompanyRepository, 
            ICompanyRepository companyRepository,
            IVehicleRepository vehicleRepository)
        {
            _parkingRepository = parkingRepository;
            _userCompanyRepository = userCompanyRepository;
            _companyRepository = companyRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ParkQuery> GetTotalsAsync(Guid userID)
        {
            var company = await _userCompanyRepository.FirstOrDefaultAsync(x => x.UserID.Equals(userID.ToString()));

            if (company == null)
            {
                throw new CustomExceptions("Estacionamento não encontrado");
            }

            var totalInput = await _parkingRepository.CountAsync(x => x.CompanyID == company.CompanyID && x.EndDate == null);
            var totalOut = await _parkingRepository.CountAsync(x => x.CompanyID == company.CompanyID && x.EndDate != null);

            return new ParkQuery { TotalInput = totalInput, TotalOut = totalOut };
        }

        public async Task EndAsync(Guid userID, ParkingModel model)
        {
            var parkModel = await GetParkModel(userID, model);
            await IsParked(parkModel.CompanyID, parkModel.VehicleID, false);

            var parked = await _parkingRepository.FirstOrDefaultAsync(x => x.VehicleID == parkModel.VehicleID && x.EndDate == null);
            parked.EndDate = DateTimeOffset.UtcNow;

            await _parkingRepository.SaveChangeAsync();
        }

        public async Task StartAsync(Guid userID, ParkingModel model)
        {

            var parkModel = await GetParkModel(userID, model);

            await IsParked(parkModel.CompanyID, parkModel.VehicleID, true);
            await HasExceededLimit(parkModel.CompanyID, parkModel.TypeID, parkModel.QtdCars, parkModel.QtdMotorcycles);

            await _parkingRepository.AddAsync(new Parking { ID = Guid.NewGuid(), CompanyID = parkModel.CompanyID, StartDate = DateTimeOffset.UtcNow, VehicleID = parkModel.VehicleID });
            await _parkingRepository.SaveChangeAsync();

        }

        private async Task<ParkModel> GetParkModel(Guid userID, ParkingModel model)
        {
            var companyID = await ValidateAsync(userID, model);
            var company = await _companyRepository.FirstOrDefaultAsync(x => x.ID == companyID);
            var vehicle = await _vehicleRepository.FirstOrDefaultAsync(x => x.Plate.Equals(model.Plate));

            return new ParkModel { CompanyID = companyID, VehicleID = vehicle.ID, QtdCars = company.QtdCars, QtdMotorcycles = company.QtdMotorcycles, TypeID = vehicle.TypeID };
        }

        private async Task HasExceededLimit(Guid companyID, byte typeID, short? qtdCars, short? qtdMotorcycles)
        {
            if (typeID == 1 && qtdCars.HasValue)
            {
                if ((await _parkingRepository.CountAsync(x => x.CompanyID == companyID && x.EndDate == null) + 1) > qtdCars.Value)
                {
                    throw new CustomExceptions("Limite de vagas de carros atingida");
                }
            }
            else
            {
                if (qtdMotorcycles.HasValue && (await _parkingRepository.CountAsync(x => x.CompanyID == companyID && x.EndDate == null) + 1) > qtdMotorcycles.Value)
                {
                    throw new CustomExceptions("Limite de vagas de mots atingida");
                }
            }
        }


        private async Task IsParked(Guid companyID, Guid vehicleID, bool isInput)
        {
            if (isInput)
            {
                if (await _parkingRepository.AnyAsync(x => x.CompanyID == companyID && x.VehicleID == vehicleID && x.EndDate == null))
                {
                    throw new CustomExceptions("Veículo dentro do estacionamento");
                }                
            } else
            {
                if (!await _parkingRepository.AnyAsync(x => x.CompanyID == companyID && x.VehicleID == vehicleID && x.EndDate == null))
                {
                    throw new CustomExceptions("Não há veículo com a placa informada para saída.");
                }
            }
        }

        private async Task<Guid> ValidateAsync(Guid userID, ParkingModel model)
        {
            if (userID == Guid.Empty)
            {
                throw new CustomExceptions("Não foi possível identificar o usuário");
            } else if (model == null)
            {
                throw new CustomExceptions("Não foi possível identificar a placa do veículo");
            }
            var userCompany = await _userCompanyRepository.FirstOrDefaultAsync(x => x.UserID.Equals(userID.ToString()));

            if (userCompany == null)
            { 
                throw new CustomExceptions("Não foi possível encontrar o estacionamento");
            }

            if (!await _vehicleRepository.AnyAsync(x => x.CompanyID == userCompany.CompanyID && x.Plate.Equals(model.Plate)))
            {
                throw new CustomExceptions("A placa não pertence ao estacionamento");
            }

            return userCompany.CompanyID;
        }
    }

    public class ParkModel
    {
        public Guid CompanyID { get; set; }
        public Guid VehicleID { get; set; }
        public short? QtdCars { get; set; }
        public short? QtdMotorcycles { get; set; }
        public byte TypeID { get; set; }
    }
}
