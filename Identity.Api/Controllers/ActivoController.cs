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
    }
}
