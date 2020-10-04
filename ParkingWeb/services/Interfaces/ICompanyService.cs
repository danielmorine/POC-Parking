using Infrastructure.Queries.Company;
using ParkingWeb.Models.Company;
using System;
using System.Threading.Tasks;

namespace ParkingWeb.services.Interfaces
{
    public interface ICompanyService
    {
        Task AddAsync(CompanyModel model);
        Task UpdateAsync(CompanyModel model);
        Task<CompanyQuery> GetByIdAsync(string userID);
        Task DeleteAsync(string CNPJ);
    }
}
