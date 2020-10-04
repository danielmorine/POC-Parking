using Infrastructure.Queries.Interfaces;
using System.Collections.Generic;

namespace Infrastructure.Queries.Park
{
    public class ParkVehiclesQuery : IQuery
    {
        public ICollection<Schemas.Vehicle> Vehicles { get; set; }
    }
}
