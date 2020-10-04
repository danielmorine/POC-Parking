using Infrastructure.Queries.Interfaces;

namespace Infrastructure.Queries.Vehicle
{
    public class VehicleQuery : IQuery
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Plate { get; set; }
    }
}
