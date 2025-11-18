using Identity.Api.DTO;
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

    public class DetalleOrdenEntregaController : Controller
    {
        private readonly IDetalleOrdenEntrega _bodega;

        public DetalleOrdenEntregaController(IDetalleOrdenEntrega iDetalleOrdenEntrega)
        {
            _bodega = iDetalleOrdenEntrega;
        }




        [HttpGet("GetDetalleOrdenEntregaById/{idDetalleOrdenEntrega}")]
        public IActionResult GetDetalleOrdenEntregaById(int idDetalleOrdenEntrega)
        {

            var bodega = _bodega.GetDetalleOrdenEntregaById(idDetalleOrdenEntrega);

            if (bodega == null)
            {
                return NotFound($"No existe esa Asignaciones Activo con el ID: {idDetalleOrdenEntrega} no encontrado.");
            }

            return Ok(bodega);
        }

        [HttpPost("InsertDetalleOrdenEntrega")]
        public IActionResult Create([FromBody] DetalleOrdenEntrega NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.InsertDetalleOrdenEntrega(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateDetalleOrdenEntrega")]
        public IActionResult Update([FromBody] DetalleOrdenEntrega UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.UpdateDetalleOrdenEntrega(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteDetalleOrdenEntrega")]
        public IActionResult Delete([FromBody] DetalleOrdenEntrega DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.DeleteDetalleOrdenEntrega(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteDetalleOrdenEntregaById/{IdDetalleOrdenEntrega}")]
        public IActionResult DeleteDetalleOrdenEntregaById(int IdDetalleOrdenEntrega)
        {
            try
            {
                _bodega.DeleteDetalleOrdenEntregaById(IdDetalleOrdenEntrega);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpGet("GetDetallesBySolicitudId/{idSolicitud}")]
        public ActionResult<IEnumerable<DetalleSolicitudDTO>> GetDetallesBySolicitudId(int idSolicitud)
        {
            try
            {
                var detalles = _bodega.GetDetallesBySolicitudId(idSolicitud);

                if (detalles == null || !detalles.Any())
                {
                    return NotFound("No se encontraron detalles para la solicitud.");
                }

                return Ok(detalles);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los detalles: " + ex.Message);
            }
        }
    }
}
