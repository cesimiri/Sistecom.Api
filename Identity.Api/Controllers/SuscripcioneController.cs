using Identity.Api.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Sistecom.Modelo.Database;
using System.Threading.Tasks;

namespace Identity.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class SuscripcioneController : ControllerBase
    {
        private readonly ISuscripcione _suscripcioneService;

        public SuscripcioneController(ISuscripcione suscripcioneService)
        {
            _suscripcioneService = suscripcioneService;
        }

        [HttpGet("SuscripcionesAll")]
        public async Task<IActionResult> GetAll()
        {
            var suscripciones = await _suscripcioneService.SuscripcionesAll();
            return Ok(suscripciones);
        }

        [HttpGet("GetSuscripcionById/{idSuscripcion}")]
        public async Task<IActionResult> GetById(int idSuscripcion)
        {
            var suscripcion = await _suscripcioneService.GetSuscripcionById(idSuscripcion);

            if (suscripcion == null)
            {
                return NotFound($"Suscripción con ID {idSuscripcion} no encontrada.");
            }

            return Ok(suscripcion);
        }

        [HttpPost("InsertSuscripcion")]
        public async Task<IActionResult> Insert([FromBody] Suscripcione newSuscripcion)
        {
            if (newSuscripcion == null || !ModelState.IsValid)
            {
                return BadRequest("Error: Datos inválidos");
            }

            await _suscripcioneService.InsertSuscripcion(newSuscripcion);
            return Ok(newSuscripcion);
        }

        [HttpPut("UpdateSuscripcion")]
        public async Task<IActionResult> Update([FromBody] Suscripcione updatedSuscripcion)
        {
            if (updatedSuscripcion == null || !ModelState.IsValid)
            {
                return BadRequest("Error: Datos inválidos");
            }

            await _suscripcioneService.UpdateSuscripcion(updatedSuscripcion);
            return NoContent();
        }

        [HttpDelete("DeleteSuscripcion")]
        public async Task<IActionResult> Delete([FromBody] Suscripcione suscripcionToDelete)
        {
            if (suscripcionToDelete == null || !ModelState.IsValid)
            {
                return BadRequest("Error: Datos inválidos");
            }

            await _suscripcioneService.DeleteSuscripcion(suscripcionToDelete);
            return NoContent();
        }

        [HttpDelete("DeleteSuscripcionById/{idSuscripcion}")]
        public async Task<IActionResult> DeleteById(int idSuscripcion)
        {
            await _suscripcioneService.DeleteSuscripcionById(idSuscripcion);
            return NoContent();
        }
    }
}
