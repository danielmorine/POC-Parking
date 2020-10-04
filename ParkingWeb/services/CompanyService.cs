using Infrastructure.Queries.Company;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Schemas;
using ParkingWeb.Enums;
using ParkingWeb.Exceptions;
using ParkingWeb.Models.Company;
using ParkingWeb.services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ParkingWeb.services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserCompanyRepository _userCompanyRepository;
        private readonly IParkingRepository _parkingRepository;

        public CompanyService(ICompanyRepository companyRepository, IParkingRepository parkingRepository, IUserCompanyRepository userCompanyRepository)
        {
            _companyRepository = companyRepository;
            _parkingRepository = parkingRepository;
            _userCompanyRepository = userCompanyRepository;
        }

        public async Task DeleteAsync(string CNPJ)
        {
            await ValidateDeleteAsync(CNPJ);

            _companyRepository.Delete(await _companyRepository.FirstOrDefaultAsync(x => x.CNPJ.Equals(CNPJ)));
            await _companyRepository.SaveChangeAsync();
        }

        public async Task<CompanyQuery> GetByIdAsync(string userID)
        {
            if (Guid.Empty.Equals(userID))
            {
                throw new CustomExceptions("Não foi possível identificar o usuário");
            }

            var result = await _userCompanyRepository.FirstOrDefaultAsync(x => x.UserID == userID, false, new string[] { "Company" });

            return new CompanyQuery
            {
                Address = result.Company.Address,
                CNPJ = result.Company.CNPJ,
                CreatedDate = result.Company.CreatedDate,
                Name = result.Company.Name,
                Phone = result.Company.Phone,
                QtdCars = result.Company.QtdCars,
                QtdMotorcycles = result.Company.QtdMotorcycles
            };
        }

        public async Task AddAsync(CompanyModel model)
        {
            await ValidateAsync(model);

            await _companyRepository.AddAsync(new Company 
            { 
                Address = model.Address,
                CNPJ = model.CNPJ,
                CreatedDate = DateTimeOffset.UtcNow,
                ID = Guid.NewGuid(),
                Name = model.Name,
                Phone = model.Phone,
                QtdCars = model.QtdCars.Value,
                QtdMotorcycles = model.QtdMotorcycles.Value,                
            });

            await _companyRepository.SaveChangeAsync();
        }

        public async Task UpdateAsync(CompanyModel model)
        {
            await ValidateUpdateAsync(model);

            var company = await _companyRepository.FirstOrDefaultAsync(x => x.CNPJ.Equals(model.CNPJ));
            
            company.Name = string.IsNullOrEmpty(model.Name) ? company.Name : model.Name;
            company.Phone = string.IsNullOrEmpty(model.Phone) ? company.Phone: model.Phone;
            company.Address = string.IsNullOrEmpty(model.Address) ? company.Address : model.Address;
            company.QtdCars = !model.QtdCars.HasValue ? company.QtdCars : await QtdAsync(company.ID, model.QtdCars.Value, TypeEnum.CARS);
            model.QtdMotorcycles = !model.QtdMotorcycles.HasValue ? company.QtdMotorcycles: await QtdAsync(company.ID, model.QtdMotorcycles.Value, TypeEnum.MOTORCYCLES);

            await _companyRepository.SaveChangeAsync();
        }

        private async Task ValidateDeleteAsync(string CNPJ)
        {
            if (!await _companyRepository.AnyAsync(x => x.CNPJ.Equals(CNPJ)))
            {
                throw new CustomExceptions("Não foi possível encontrar o cnpj informado");
            }
            else if (await _companyRepository.AnyAsync(x => x.CNPJ.Equals(CNPJ) && x.Parkings.Count > 0, new string[] { "Parkings" }))
            {
                throw new CustomExceptions("Não foi possível apagar um estacionamento com veículos alocados");
            }
            else if (await _companyRepository.AnyAsync(x => x.CNPJ.Equals(CNPJ) && x.Vehicles.Count > 0, new string[] { "Vehicles" }))
            {
                throw new CustomExceptions("Não foi possível apagar um estacionamento com veículos cadastrados");
            }
            else if (await _userCompanyRepository.AnyAsync(x => x.Company.CNPJ.Equals(CNPJ), new string[] { "Company" }))
            {
                throw new CustomExceptions("Não foi possível apagar um estacionamento com usuários cadastrados");
            }
        }
        private async Task<short> QtdAsync(Guid companyID, short value, TypeEnum type)
        {
            if (type == TypeEnum.CARS)
            {
                var count = await _parkingRepository.CountAsync(x => x.CompanyID == companyID && x.EndDate == null && x.Vehicle.TypeID == 1, new string[] { "Vehicle"} );

                if (value < count)
                {
                    throw new CustomExceptions($"Não é possível reduzir a capacidade total de carros no estacionamento pois há {count} carros");
                }

            } else if (type == TypeEnum.MOTORCYCLES)
            {
                var count = await _parkingRepository.CountAsync(x => x.CompanyID == companyID && x.EndDate == null && x.Vehicle.TypeID == 2, new string[] { "Vehicle" });

                if (value < count)
                {
                    throw new CustomExceptions($"Não é possível reduzir a capacidade total de motos no estacionamento pois há {count} carros");
                }
            }

            return value;
        }

        private async Task ValidateUpdateAsync(CompanyModel model)
        {
            if (model == null)
            {
                throw new CustomExceptions("Não foi possível atualizar este registro, verifique os dados informados");
            } else if (!string.IsNullOrEmpty(model.CNPJ)  && !await _companyRepository.AnyAsync(x => x.CNPJ.Equals(model.CNPJ)))
            {
                throw new CustomExceptions("Não foi possível encontrar o CNPJ informado");
            }
        }

        private async Task ValidateAsync(CompanyModel model)
        {
            if (model == null)
            {
                throw new CustomExceptions("Verifique os campos informados");
            } else if (await _companyRepository.AnyAsync(x => x.CNPJ.Equals(model.CNPJ)))
            {
                throw new CustomExceptions("O CNPJ informado já está em uso");
            }

        }
        
    }
}
