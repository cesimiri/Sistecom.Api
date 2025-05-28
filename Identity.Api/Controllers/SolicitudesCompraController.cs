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
    public class SolicitudesCompraController : Controller
    {
        private readonly ISolicitudesCompra _solicitudesCompraService;

        public SolicitudesCompraController(ISolicitudesCompra solicitudesCompraService)
        {
            _solicitudesCompraService = solicitudesCompraService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("SolicitudesCompraAll")]
        public IActionResult GetAll()
        {
            return Ok(_solicitudesCompraService.SolicitudesCompraAll);
        }

        [HttpGet("GetSolicitudById/{idSolicitud}")]
        public IActionResult GetById(int idSolicitud)
        {
            var solicitud = _solicitudesCompraService.GetSolicitudById(idSolicitud);

            if (solicitud == null)
            {
                return NotFound($"Solicitud con ID {idSolicitud} no encontrada.");
            }

            return Ok(solicitud);
        }

        [HttpPost("InsertSolicitud")]
        public IActionResult Insert([FromBody] SolicitudesCompra newSolicitud)
        {
            if (newSolicitud == null || !ModelState.IsValid)
            {
                return BadRequest("Error: Datos inválidos");
            }

            _solicitudesCompraService.InsertSolicitud(newSolicitud);
            return Ok(newSolicitud);
        }

        [HttpPut("UpdateSolicitud")]
        public IActionResult Update([FromBody] SolicitudesCompra updatedSolicitud)
        {
            if (updatedSolicitud == null || !ModelState.IsValid)
            {
                return BadRequest("Error: Datos inválidos");
            }

            _solicitudesCompraService.UpdateSolicitud(updatedSolicitud);
            return NoContent();
        }

        [HttpDelete("DeleteSolicitud")]
        public IActionResult Delete([FromBody] SolicitudesCompra solicitudToDelete)
        {
            if (solicitudToDelete == null || !ModelState.IsValid)
            {
                return BadRequest("Error: Datos inválidos");
            }

            _solicitudesCompraService.DeleteSolicitud(solicitudToDelete);
            return NoContent();
        }

        [HttpDelete("DeleteSolicitud/{idSolicitud}")]
        public IActionResult DeleteById(int idSolicitud)
        {
            _solicitudesCompraService.DeleteSolicitudById(idSolicitud);
            return NoContent();
        }
    }
}
