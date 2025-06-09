using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SuscripcioneController : ControllerBase
    {
        private readonly ISuscripcione _suscripcioneService;

        public SuscripcioneController(ISuscripcione suscripcioneService)
        {
            _suscripcioneService = suscripcioneService;
        }

        [HttpGet("SuscripcionesAll")]
        public IActionResult GetAll()
        {
            var suscripciones = _suscripcioneService.GetAllSuscripciones();
            return Ok(suscripciones);
        }

        [HttpGet("GetSuscripcionById/{idSuscripcion}")]
        public IActionResult GetById(int idSuscripcion)
        {
            var suscripcion = _suscripcioneService.GetSuscripcionById(idSuscripcion);
            if (suscripcion == null)
            {
                return NotFound($"Suscripción con ID {idSuscripcion} no encontrada.");
            }
            return Ok(suscripcion);
        }

        [HttpPost("InsertSuscripcion")]
        public IActionResult InsertSuscripcion([FromBody] SuscripcionDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                _suscripcioneService.InsertSuscripcion(dto);
                return Ok(new { message = "Suscripción guardada exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("UpdateSuscripcion")]
        public IActionResult Update([FromBody] SuscripcionDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                _suscripcioneService.UpdateSuscripcion(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("DeleteSuscripcion")]
        public IActionResult Delete([FromBody] SuscripcionDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                _suscripcioneService.DeleteSuscripcion(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("DeleteSuscripcionById/{idSuscripcion}")]
        public IActionResult DeleteById(int idSuscripcion)
        {
            try
            {
                _suscripcioneService.DeleteSuscripcionById(idSuscripcion);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
