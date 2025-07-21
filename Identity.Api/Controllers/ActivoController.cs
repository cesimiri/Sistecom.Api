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

    public class ActivoController : Controller
    {
        private readonly IActivo _empresaCliente;

        public ActivoController(IActivo iActivo)
        {
            _empresaCliente = iActivo;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("ActivoInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_empresaCliente.ActivoInfoAll);
        }


        [HttpGet("GetActivoById/{IdActivo}")]
        public IActionResult GetActivoById(int IdActivo)
        {

            var empresaCliente = _empresaCliente.GetActivoById(IdActivo);

            if (empresaCliente == null)
            {
                return NotFound($"No se escontro Activo con el ID:{IdActivo} no encontrado.");
            }

            return Ok(empresaCliente);
        }

        [HttpPost("InsertActivo")]
        public IActionResult Create([FromBody] Activo NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.InsertActivo(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateActivo")]
        public IActionResult Update([FromBody] Activo UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.UpdateActivo(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteActivo")]
        public IActionResult Delete([FromBody] Activo DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.DeleteActivo(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteActivoById/{IdActivo}")]
        public IActionResult DeleteById(int IdActivo)
        {
            try
            {
                _empresaCliente.DeleteActivoById(IdActivo);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }


        //PAGINADO
        [HttpGet("GetPaginados")]
        public IActionResult GetPaginados(
            int pagina = 1,
            int pageSize = 8,
            string? codigoActivo = null,
            int? idProducto = null,
            DateTime? desde = null,
            DateTime? hasta = null,
            int? idFacturaCompra = null,
            string? estadoActivo = null,
            string? ordenColumna = null,
            bool ordenAscendente = true)
        {
            var resultado = _empresaCliente.GetPaginados(
                pagina, pageSize,
                codigoActivo, idProducto,
                desde, hasta,
                idFacturaCompra, estadoActivo,
                ordenColumna, ordenAscendente);

            return Ok(resultado);
        }


    }
}
