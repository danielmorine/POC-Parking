using Infrastructure.Queries.Company;
using ParkingWeb.Models.Company;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingWeb.services.Interfaces
{
    public interface ICompanyService
    {
        Task AddAsync(CompanyModel model);
        Task UpdateAsync(CompanyModel model);
        Task DeleteAsync(string CNPJ);
        Task<IEnumerable<CompanyQuery>> GetAllAsync();
    }
}
