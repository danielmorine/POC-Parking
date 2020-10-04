namespace ParkingWeb.Models.Company
{
    public class CompanyModel
    {
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public short? QtdCars { get; set; }
        public short? QtdMotorcycles { get; set; }
    }
}
