namespace ParkingWeb.Models.Vehicle
{
    public class VehicleUpdateModel
    {
        public byte? TypeID { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public string Color { get; set; }
        public string Plate { get; set; }
    }
}
