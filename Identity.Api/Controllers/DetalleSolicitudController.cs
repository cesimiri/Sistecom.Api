using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Services;
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

        //[HttpDelete("DeleteDetalleSolicitud")]
        //public IActionResult Delete([FromBody] DetalleSolicitud delItem)
        //{
        //    try
        //    {
        //        if (delItem == null || !ModelState.IsValid)
        //        {
        //            return BadRequest("Error: Envío de datos inválido");
        //        }

        //        _detalleSolicitud.DeleteDetalleSolicitud(delItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error:" + ex.Message);
        //    }

        //    return NoContent();
        //}

        [HttpDelete("DeleteDetalleSolicitud/{idDetalle}")]
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
    }
}
