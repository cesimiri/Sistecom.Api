using Identity.Api.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class SuscripcioneController : Controller
    {
        private readonly ISuscripcione _suscripcioneService;

        public SuscripcioneController(ISuscripcione suscripcioneService)
        {
            _suscripcioneService = suscripcioneService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("SuscripcionesAll")]
        public IActionResult GetAll()
        {
            return Ok(_suscripcioneService.SuscripcionesAll);
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
        public IActionResult Insert([FromBody] Suscripcione newSuscripcion)
        {
            if (newSuscripcion == null || !ModelState.IsValid)
            {
                return BadRequest("Error: Datos inválidos");
            }

            _suscripcioneService.InsertSuscripcion(newSuscripcion);
            return Ok(newSuscripcion);
        }

        [HttpPut("UpdateSuscripcion")]
        public IActionResult Update([FromBody] Suscripcione updatedSuscripcion)
        {
            if (updatedSuscripcion == null || !ModelState.IsValid)
            {
                return BadRequest("Error: Datos inválidos");
            }

            _suscripcioneService.UpdateSuscripcion(updatedSuscripcion);
            return NoContent();
        }

        [HttpDelete("DeleteSuscripcion")]
        public IActionResult Delete([FromBody] Suscripcione suscripcionToDelete)
        {
            if (suscripcionToDelete == null || !ModelState.IsValid)
            {
                return BadRequest("Error: Datos inválidos");
            }

            _suscripcioneService.DeleteSuscripcion(suscripcionToDelete);
            return NoContent();
        }

        [HttpDelete("DeleteSuscripcion/{idSuscripcion}")]
        public IActionResult DeleteById(int idSuscripcion)
        {
            _suscripcioneService.DeleteSuscripcionById(idSuscripcion);
            return NoContent();
        }
    }
}
