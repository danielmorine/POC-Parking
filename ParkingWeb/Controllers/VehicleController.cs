using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingWeb.Exceptions;
using ParkingWeb.Models.Vehicle;
using ParkingWeb.services.Interfaces;

namespace ParkingWeb.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] VehicleModel model)
        {
            try
            {
                await _vehicleService.AddAsync(model, Guid.Parse(User.Identity.Name));
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

        [HttpGet("getall/format.{format}"), FormatFilter]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _vehicleService.GetAllAsync(Guid.Parse(User.Identity.Name)));
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

        [HttpGet("plate/{plate}/format.{format}"), FormatFilter]
        public async Task<IActionResult> GetAsync([FromRoute] string plate)
        {
            try
            {
                return Ok(await _vehicleService.GetByPlateAsync(plate, Guid.Parse(User.Identity.Name)));
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

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] VehicleUpdateModel model)
        {
            try
            {
                await _vehicleService.UpdateAsync(model, Guid.Parse(User.Identity.Name));
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

        [HttpDelete]
        [Route("delete/{plate}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string plate)
        {
            try
            {
                await _vehicleService.DeleteAsync(plate, Guid.Parse(User.Identity.Name));
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
