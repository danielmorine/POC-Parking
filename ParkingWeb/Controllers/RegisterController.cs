using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingWeb.Exceptions;
using ParkingWeb.Models.Register;
using ParkingWeb.services.Interfaces;

namespace ParkingWeb.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "AdministratorBearer")]
    
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterModel model)
        {
            try
            {
                await _registerService.AddAsync(model);
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
