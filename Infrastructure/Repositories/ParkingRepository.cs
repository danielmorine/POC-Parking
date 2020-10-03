using Infrastructure.Interfaces;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Schemas;

namespace Infrastructure.Repositories
{
    public class ParkingRepository : RepositoryBase<Parking>, IParkingRepository
    {
        public ParkingRepository(IApplicationDbContext db) :base(db) { }
    }
}
