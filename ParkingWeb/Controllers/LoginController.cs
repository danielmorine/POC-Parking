using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingWeb.Exceptions;

namespace ParkingWeb.Controllers
{
    [Route("api/v1/[controller]")]
    public class LoginController : ControllerBase
    {        

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            try
            {
                return Ok();
            }
            catch (CustomExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
