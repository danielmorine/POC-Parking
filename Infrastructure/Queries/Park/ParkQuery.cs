using Infrastructure.Queries.Interfaces;

namespace Infrastructure.Queries.Park
{
    public class ParkQuery : IQuery
    {
        public int TotalInput { get; set; }
        public int TotalOut { get; set; }
    }
}
