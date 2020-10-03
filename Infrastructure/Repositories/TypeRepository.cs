using Infrastructure.Interfaces;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Schemas;

namespace Infrastructure.Repositories
{
    public class TypeRepository : RepositoryBase<Type>, ITypeRepository
    {
        public TypeRepository(IApplicationDbContext db) : base(db) { }
    }
}
