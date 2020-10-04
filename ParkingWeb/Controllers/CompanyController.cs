using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingWeb.Exceptions;
using ParkingWeb.Models.Company;
using ParkingWeb.services.Interfaces;

namespace ParkingWeb.Controllers
{
    [Route("api/v1/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        [Authorize(Policy = "AdministratorBearer")]
        public async Task<IActionResult> PostAsync([FromBody] CompanyModel model)
        {
            try
            {
                await _companyService.AddAsync(model);
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

        [HttpGet]
        [Authorize(Policy = "UserBearer")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                return Ok(await _companyService.GetByIdAsync(User.Identity.Name));
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
        [Authorize(Policy = "UserBearer")]
        public async Task<IActionResult> PutAsync([FromBody] CompanyModel model)
        {
            try
            {
                await _companyService.UpdateAsync(model);
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
        [Route("delete/{CNPJ}")]
        [Authorize(Policy = "AdministratorBearer")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string CNPJ)
        {
            try
            {
                await _companyService.DeleteAsync(CNPJ);
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
