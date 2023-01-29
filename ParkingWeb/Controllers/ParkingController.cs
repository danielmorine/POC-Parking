using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingWeb.Exceptions;
using ParkingWeb.Models.Parking;
using ParkingWeb.services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ParkingWeb.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ParkingModel model)
        {
            try
            {
                await _parkingService.StartAsync(Guid.Parse(User.Identity.Name), model);
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

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ParkingModel model)
        {
            try
            {
                await _parkingService.EndAsync(Guid.Parse(User.Identity.Name), model);
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

        [HttpGet("gettotals/format.{format}"), FormatFilter]
        public async Task<IActionResult> GetTotalsAsync()
        {
            try
            {
                return Ok(await _parkingService.GetTotalsAsync(Guid.Parse(User.Identity.Name)));
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

        [HttpGet("getAll/format.{format}"), FormatFilter]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _parkingService.GetTotalsAsync(Guid.Parse(User.Identity.Name)));
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
