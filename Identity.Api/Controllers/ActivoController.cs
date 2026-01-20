using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Controllers
{
    //[Authorize]
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


        [HttpPost("InsertarActivos")]
        public async Task<IActionResult> InsertarActivos([FromBody] List<ActivoDTO> activos)
        {
            var responses = await _empresaCliente.InsertActivos(activos);
            return Ok(responses);
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

        // Update 1 a 1 
        [HttpPut("Update1a1")]
        public IActionResult Update1a1([FromBody] ActivoDTO UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.Update1a1(UpdItem);
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
        [HttpGet("GetActivoPaginados")]
        public IActionResult GetActivoPaginados(
            int pagina = 1,
            int pageSize = 8,
            string? codigoActivo = null,
            string? estadoActivo = null)
        {
            var resultado = _empresaCliente.GetActivoPaginados(
                pagina, pageSize, codigoActivo, estadoActivo);

            return Ok(resultado);
        }


    }
}
