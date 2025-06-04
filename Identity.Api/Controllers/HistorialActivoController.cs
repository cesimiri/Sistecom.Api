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

    public class HistorialActivoController : Controller
    {
        private readonly IHistorialActivo _empresaCliente;

        public HistorialActivoController(IHistorialActivo iHistorialActivo)
        {
            _empresaCliente = iHistorialActivo;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("HistorialActivoInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_empresaCliente.HistorialActivoInfoAll);
        }


        [HttpGet("GetHistorialActivoById/{idIHistorialActivo}")]
        public IActionResult GetIHistorialActivoById(int idIHistorialActivo)
        {
            //var proveedor = _proveedorService.GetProveedorById(idProveedor);

            var empresaCliente = _empresaCliente.GetHistorialActivoById(idIHistorialActivo);

            if (empresaCliente == null)
            {
                return NotFound($"Empresa Cliente con ID {idIHistorialActivo} no encontrado.");
            }

            return Ok(empresaCliente);
        }

        [HttpPost("InsertHistorialActivo")]
        public IActionResult Create([FromBody] HistorialActivo NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.InsertHistorialActivo(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateHistorialActivo")]
        public IActionResult Update([FromBody] HistorialActivo UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.UpdateHistorialActivo(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteHistorialActivo")]
        public IActionResult Delete([FromBody] HistorialActivo DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.DeleteHistorialActivo(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteHistorialActivo/{IdHistorialActivo}")]
        public IActionResult DeleteById(int IdHistorialActivo)
        {
            try
            {
                _empresaCliente.DeleteHistorialActivoById(IdHistorialActivo);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }
    }
}
