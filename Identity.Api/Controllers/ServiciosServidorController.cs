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

        //trae todo los servidores por Activo
        [HttpGet("GetSetvidores")]
        public IActionResult GetSetvidores()
        {
            return Ok(_bodega.GetSetvidores);
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
        public IActionResult Create([FromBody] ServiciosServidorDTO NewItem)
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

        //paginado
        [HttpGet("GetServiciosServidorPaginados")]
        public IActionResult GetServiciosServidorPaginados(
        int pagina = 1,
        int pageSize = 8,
        string? codigoActivo = null,
        string? estadoActivo = null)
        {
            try
            {
                var resultado = _bodega.GetServiciosServidorPaginados(
                    pagina, pageSize, codigoActivo, estadoActivo);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error interno: {ex.Message}" });
            }
        }

    }
}
