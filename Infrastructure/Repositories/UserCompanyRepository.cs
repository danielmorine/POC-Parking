using Infrastructure.Interfaces;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Schemas;

namespace Infrastructure.Repositories
{
    public class UserCompanyRepository : RepositoryBase<UserCompany>, IUserCompanyRepository
    {
        public UserCompanyRepository(IApplicationDbContext db) : base(db) { }
    }
}
