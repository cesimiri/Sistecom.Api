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

    public class DetalleSolicitudController : Controller
    {
        private readonly IDetalleSolicitud _detalleSolicitud;

        public DetalleSolicitudController(IDetalleSolicitud detalleSolicitud)
        {
            _detalleSolicitud = detalleSolicitud;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("DetalleSolicitudesAll")]
        public IActionResult GetAll()
        {
            return Ok(_detalleSolicitud.DetalleSolicitudesAll);
        }



        [HttpGet("GetDetalleSolicitudById/{idDetalle}")]
        public IActionResult GetDetalleSolicitudById(int idDetalle)
        {
            var detalle = _detalleSolicitud.GetDetalleSolicitudById(idDetalle);

            if (detalle == null)
            {
                return NotFound($"DetalleSolicitud con ID {idDetalle} no encontrado.");
            }

            return Ok(detalle);
        }

        //para traer todos lso registro por la solicitud de compra 
        [HttpGet("GetDetalleSolicitudByIdSolicitud/{idSolicitud}")]
        public IActionResult GetDetalleSolicitudByIdSolicitud(int idSolicitud)
        {
            var detalle = _detalleSolicitud.GetDetalleSolicitudByIdSolicitud(idSolicitud);

            if (detalle == null || !detalle.Any())
            {
                return NotFound($"No se encontraron detalles para la solicitud {idSolicitud}.");
            }

            return Ok(detalle);
        }

        [HttpPost("InsertDetalleSolicitud")]
        public IActionResult Create([FromBody] DetalleSolicitudDTO newItem)
        {
            try
            {
                if (newItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envío de datos inválido");
                }

                _detalleSolicitud.InsertDetalleSolicitud(newItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(newItem);
        }

        //TRAE TODAS LAS SOLICITUDES DE COMPRAR DIFERENTE AL ESTAD RECHAZADA COMPLETADA Y CANCELADA
        [HttpGet("SolicitudesDeCompraPorEstado")]
        public IActionResult SolicitudesDeCompraPorEstadoAsync()
        {
            return Ok(_detalleSolicitud.SolicitudesDeCompraPorEstadoAsync());
        }



        //INGRESO MASIVO
        [HttpPost("InsertarDetallesMasivos")]
        public IActionResult InsertarDetallesMasivos([FromBody] List<DetalleSolicitudDTO> lista)
        {
            try
            {
                if (lista == null || !lista.Any())
                    return BadRequest("La lista está vacía o es nula.");

                _detalleSolicitud.InsertarDetallesMasivos(lista);
                return Ok("Inserción masiva completada.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error en la inserción masiva: " + ex.Message);
            }
        }


        [HttpPut("UpdateDetalleSolicitud")]
        public IActionResult Update([FromBody] DetalleSolicitudDTO updItem)
        {
            try
            {
                if (updItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envío de datos inválido");
                }

                _detalleSolicitud.UpdateDetalleSolicitud(updItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }


        [HttpDelete("DeleteDetalleSolicitudById/{idDetalle}")]
        public IActionResult DeleteById(int idDetalle)
        {
            try
            {
                _detalleSolicitud.DeleteDetalleSolicitudById(idDetalle);
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
                var detalles = _detalleSolicitud.GetDetallesBySolicitudId(idSolicitud);

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
