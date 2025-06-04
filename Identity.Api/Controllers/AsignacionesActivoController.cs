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

    public class AsignacionesActivoController : Controller
    {
        private readonly IAsignacionesActivo _bodega;

        public AsignacionesActivoController(IAsignacionesActivo iAsignacionesActivo)
        {
            _bodega = iAsignacionesActivo;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("AsignacionesActivoInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_bodega.AsignacionesActivoInfoAll);
        }


        [HttpGet("GetAsignacionesActivoById/{idAsignacionesActivo}")]
        public IActionResult GetAsignacionesActivoById(int idAsignacionesActivo)
        {

            var bodega = _bodega.GetAsignacionesActivoById(idAsignacionesActivo);

            if (bodega == null)
            {
                return NotFound($"No existe esa Asignaciones Activo con el ID: {idAsignacionesActivo} no encontrado.");
            }

            return Ok(bodega);
        }

        [HttpPost("InsertAsignacionesActivo")]
        public IActionResult Create([FromBody] AsignacionesActivo NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.InsertAsignacionesActivo(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateAsignacionesActivo")]
        public IActionResult Update([FromBody] AsignacionesActivo UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.UpdateAsignacionesActivo(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteAsignacionesActivo")]
        public IActionResult Delete([FromBody] AsignacionesActivo DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.DeleteAsignacionesActivo(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteAsignacionesActivoById/{IdAsignacionesActivo}")]
        public IActionResult DeleteAsignacionesActivoById(int IdAsignacionesActivo)
        {
            try
            {
                _bodega.DeleteAsignacionesActivoById(IdAsignacionesActivo);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }
    }
}
