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
    public class AsignacionesLicenciaController : Controller
    {
        private readonly IAsignacionesLicencia _bodega;

        public AsignacionesLicenciaController(IAsignacionesLicencia iAsignacionesLicencia)
        {
            _bodega = iAsignacionesLicencia;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("AsignacionesLicenciaInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_bodega.AsignacionesLicenciaInfoAll);
        }


        [HttpGet("GetAsignacionesLicenciaById/{idAsignacionesLicencia}")]
        public IActionResult GetAsignacionesLicenciaById(int idAsignacionesLicencia)
        {

            var bodega = _bodega.GetAsignacionesLicenciaById(idAsignacionesLicencia);

            if (bodega == null)
            {
                return NotFound($"No existe esa Asignaciones Activo con el ID: {idAsignacionesLicencia} no encontrado.");
            }

            return Ok(bodega);
        }

        [HttpPost("InsertAsignacionesLicencia")]
        public IActionResult Create([FromBody] AsignacionesLicencia NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.InsertAsignacionesLicencia(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateAsignacionesLicencia")]
        public IActionResult Update([FromBody] AsignacionesLicencia UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.UpdateAsignacionesLicencia(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteAsignacionesLicencia")]
        public IActionResult Delete([FromBody] AsignacionesLicencia DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.DeleteAsignacionesLicencia(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteAsignacionesLicenciaById/{IdAsignacionesLicencia}")]
        public IActionResult DeleteAsignacionesLicenciaById(int IdAsignacionesLicencia)
        {
            try
            {
                _bodega.DeleteAsignacionesLicenciaById(IdAsignacionesLicencia);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }
    }
}
