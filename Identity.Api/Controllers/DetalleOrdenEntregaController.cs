using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class DetalleOrdenEntregaController : Controller
    {
        private readonly IDetalleOrdenEntrega _bodega;

        public DetalleOrdenEntregaController(IDetalleOrdenEntrega bodega)
        {
            _bodega = bodega;
        }

        [HttpPost("InsertDetalleOrdenEntregaMasivo")]
        public IActionResult Create([FromBody] List<DetalleOrdenEntregaDTO> lista)
        {
            try
            {
                if (lista == null || lista.Count == 0)
                    return BadRequest("Debe enviar al menos un detalle.");

                _bodega.InsertDetalleOrdenEntregaMasivo(lista);

                return Ok(new
                {
                    Message = "Detalles insertados correctamente.",
                    IdOrden = lista[0].IdOrden,
                    CantidadInsertada = lista.Count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("DeleteDetalleOrdenEntregaById/{idOrden}")]
        public IActionResult DeleteDetalleOrdenEntregaById(int idOrden)
        {
            try
            {
                _bodega.DeleteDetalleOrdenEntregaByIdOrden(idOrden);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        //nos trae toda las lineas por el número de solicitud para poder editarla 
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

        //por id
        [HttpGet("GetDetallesOrdenEntregaById/{idOrden}")]
        public ActionResult<IEnumerable<DetalleSolicitudDTO>> GetDetallesOrdenEntregaById(int idOrden)
        {
            try
            {
                var detalles = _bodega.GetDetallesOrdenEntregaById(idOrden);

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
