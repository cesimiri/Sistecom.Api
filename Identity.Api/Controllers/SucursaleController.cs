using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SucursaleController : Controller
    {
        private readonly ISucursale _iSucursale;

        public SucursaleController(ISucursale iSucursale)
        {
            _iSucursale = iSucursale;
        }

        [HttpGet("GetAllSucursale")]
        public IActionResult GetAll()
        {
            var sucursales = _iSucursale.GetAllSucursale();
            return Ok(sucursales);
        }

        [HttpGet("GetSucursaleById/{idSucursal}")]
        public IActionResult GetById(int idSucursal)
        {
            var sucurasales = _iSucursale.GetSucursaleById(idSucursal);
            if (sucurasales == null)
            {
                return NotFound($"Suscripción con ID {idSucursal} no encontrada.");
            }
            return Ok(sucurasales);
        }

        [HttpPost("InsertSucursale")]
        public IActionResult InsertSucursale([FromBody] SucursaleDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                _iSucursale.InsertSucursale(dto);
                return Ok(new { message = "Sucursal guardada exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("UpdateSucursale")]
        public IActionResult Update([FromBody] SucursaleDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                _iSucursale.UpdateSucursale(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        //[HttpDelete("DeleteSuscripcion")]
        //public IActionResult Delete([FromBody] SuscripcionDto dto)
        //{
        //    if (dto == null || !ModelState.IsValid)
        //    {
        //        return BadRequest("Datos inválidos.");
        //    }

        //    try
        //    {
        //        _suscripcioneService.DeleteSuscripcion(dto);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { error = ex.Message });
        //    }
        //}

        [HttpDelete("DeleteSucursaleById/{idSucursal}")]
        public IActionResult DeleteById(int idSucursal)
        {
            try
            {
                _iSucursale.DeleteSucursaleById(idSucursal);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
