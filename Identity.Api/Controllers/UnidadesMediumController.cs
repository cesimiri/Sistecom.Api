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
    public class UnidadesMediumController : Controller
    {
        private readonly IUnidadesMedidum _iUnidades;

        public UnidadesMediumController(IUnidadesMedidum iUnidades)
        {
            _iUnidades = iUnidades;
        }

        [HttpGet("GetAllUnidades")]
        public IActionResult GetAll()
        {
            var unidades = _iUnidades.GetAllUnidades();
            return Ok(unidades);
        }

        [HttpGet("GetUnidadesById/{idUnidades}")]
        public IActionResult GetById(int idUnidades)
        {
            var sucurasales = _iUnidades.GetUnidadesById(idUnidades);
            if (sucurasales == null)
            {
                return NotFound($"unidades con ID {idUnidades} no encontrada.");
            }
            return Ok(sucurasales);
        }

        [HttpPost("InsertUnidades")]
        public IActionResult InsertUnidades([FromBody] UnidadesMedidumDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                _iUnidades.InsertUnidades(dto);
                return Ok(new { message = "Unidad de medida guardada exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("UpdateUnidades")]
        public IActionResult Update([FromBody] UnidadesMedidum dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                _iUnidades.UpdateUnidades(dto);
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

        [HttpDelete("DeleteUnidadesById/{idUnidades}")]
        public IActionResult DeleteById(int idUnidades)
        {
            try
            {
                _iUnidades.DeleteUnidadesById(idUnidades);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
