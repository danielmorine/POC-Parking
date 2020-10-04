using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ParkingWeb.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "AdministratorBearer")]

    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("OK");
        }
    }
}
