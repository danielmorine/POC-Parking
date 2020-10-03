namespace ParkingWeb.Configurations
{
    public class TokenConfiguration
    {
        public string Audience { get; } = "PocParking";
        public string Issuer { get; } = "PocParkingIssuer";
        public int Seconds { get; } = 1800;
    }
}
