using Microsoft.AspNetCore.Mvc;

namespace ParkingWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("OK");
        }
    }
}
