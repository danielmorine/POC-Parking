using Infrastructure.Interfaces;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Schemas;

namespace Infrastructure.Repositories
{
    public class VehicleRepository : RepositoryBase<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(IApplicationDbContext db) : base(db) { }
    }
}
