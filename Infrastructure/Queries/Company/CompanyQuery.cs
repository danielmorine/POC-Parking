using Infrastructure.Queries.Interfaces;
using System;

namespace Infrastructure.Queries.Company
{
    public class CompanyQuery : IQuery
    {
        public string CNPJ { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public short? QtdCars { get; set; }
        public short? QtdMotorcycles { get; set; }
    }
}
