using Identity.Api.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Controllers
{
<<<<<<< HEAD
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SuscripcioneController : ControllerBase
=======
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class SuscripcioneController : Controller
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
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
            if (!ModelState.IsValid)
            {
                var errores = ModelState
                    .Where(ms => ms.Value?.Errors.Count > 0)
                    .Select(kvp => new
                    {
                        Campo = kvp.Key,
                        Errores = kvp.Value?.Errors.Select(e => e.ErrorMessage)
                    });

                return BadRequest(new
                {
                    mensaje = "Errores de validación",
                    detalles = errores
                });
            }

<<<<<<< HEAD
            try
            {
                // El service se encarga de toda la validación y lógica de negocio
                await _suscripcioneService.InsertSuscripcion(newSuscripcion);

                // Obtener la entidad completa con las navegaciones cargadas
                var suscripcionCreada = await _suscripcioneService.GetSuscripcionById(newSuscripcion.IdSuscripcion);

                return Ok(suscripcionCreada);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = "Error al insertar la suscripción",
                    detalle = ex.Message
                });
            }
=======
            _suscripcioneService.InsertSuscripcion(newSuscripcion);
            return Ok(newSuscripcion);
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
        }

        [HttpPut("UpdateSuscripcion")]
        public IActionResult Update([FromBody] Suscripcione updatedSuscripcion)
        {
            if (updatedSuscripcion == null || !ModelState.IsValid)
            {
                return BadRequest("Error: Datos inválidos");
            }

<<<<<<< HEAD
            try
            {
                // El service se encarga de toda la validación y lógica de negocio
                await _suscripcioneService.UpdateSuscripcion(updatedSuscripcion);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = "Error al actualizar la suscripción",
                    detalle = ex.Message
                });
            }
=======
            _suscripcioneService.UpdateSuscripcion(updatedSuscripcion);
            return NoContent();
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
        }

        [HttpDelete("DeleteSuscripcion")]
        public IActionResult Delete([FromBody] Suscripcione suscripcionToDelete)
        {
            if (suscripcionToDelete == null || !ModelState.IsValid)
            {
                return BadRequest("Error: Datos inválidos");
            }

<<<<<<< HEAD
            try
            {
                await _suscripcioneService.DeleteSuscripcion(suscripcionToDelete);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = "Error al eliminar la suscripción",
                    detalle = ex.Message
                });
            }
=======
            _suscripcioneService.DeleteSuscripcion(suscripcionToDelete);
            return NoContent();
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
        }

        [HttpDelete("DeleteSuscripcion/{idSuscripcion}")]
        public IActionResult DeleteById(int idSuscripcion)
        {
<<<<<<< HEAD
            try
            {
                await _suscripcioneService.DeleteSuscripcionById(idSuscripcion);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = "Error al eliminar la suscripción",
                    detalle = ex.Message
                });
            }
        }

        // Endpoints adicionales para obtener proveedores y empresas (útiles para el frontend)
        [HttpGet("GetProveedores")]
        public async Task<IActionResult> GetProveedores()
        {
            var proveedores = await _suscripcioneService.GetProveedoreAsync();
            return Ok(proveedores);
        }

        [HttpGet("GetEmpresas")]
        public async Task<IActionResult> GetEmpresas()
        {
            var empresas = await _suscripcioneService.GetEmpresaClienteAsync();
            return Ok(empresas);
=======
            _suscripcioneService.DeleteSuscripcionById(idSuscripcion);
            return NoContent();
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
        }
    }
}