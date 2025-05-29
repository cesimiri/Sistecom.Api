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
    public class ServiciosServidorController : Controller
    {
        private readonly IServiciosServidor _bodega;

        public ServiciosServidorController(IServiciosServidor iServiciosServidor)
        {
            _bodega = iServiciosServidor;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("ServiciosServidorInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_bodega.ServiciosServidorInfoAll);
        }


        [HttpGet("GetServiciosServidorById/{idServiciosServidor}")]
        public IActionResult GetServiciosServidorById(int idServiciosServidor)
        {

            var bodega = _bodega.GetServiciosServidorById(idServiciosServidor);

            if (bodega == null)
            {
                return NotFound($"No existe esa Asignaciones Activo con el ID: {idServiciosServidor} no encontrado.");
            }

            return Ok(bodega);
        }

        [HttpPost("InsertServiciosServidor")]
        public IActionResult Create([FromBody] ServiciosServidor NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.InsertServiciosServidor(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateServiciosServidor")]
        public IActionResult Update([FromBody] ServiciosServidor UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.UpdateServiciosServidor(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteServiciosServidor")]
        public IActionResult Delete([FromBody] ServiciosServidor DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.DeleteServiciosServidor(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteServiciosServidorById/{IdServiciosServidor}")]
        public IActionResult DeleteServiciosServidorById(int IdServiciosServidor)
        {
            try
            {
                _bodega.DeleteServiciosServidorById(IdServiciosServidor);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }
    }
}
