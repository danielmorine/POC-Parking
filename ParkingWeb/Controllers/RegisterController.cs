﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingWeb.Exceptions;
using ParkingWeb.Models.Register;

namespace ParkingWeb.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "AdministratorBearer")]
    
    public class RegisterController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterModel model)
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