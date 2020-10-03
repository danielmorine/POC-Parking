using Infrastructure.Interfaces;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Schemas;

namespace Infrastructure.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(IApplicationDbContext db) : base(db) { }
    }
}
