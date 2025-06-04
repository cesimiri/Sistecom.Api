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

    public class MantenimientoController : Controller
    {
        private readonly IMantenimiento _empresaCliente;

        public MantenimientoController(IMantenimiento iMantenimiento)
        {
            _empresaCliente = iMantenimiento;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("MantenimientoInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_empresaCliente.MantenimientoInfoAll);
        }


        [HttpGet("GetMantenimientoById/{IdMantenimiento}")]
        public IActionResult GetMantenimientoById(int IdMantenimiento)
        {

            var empresaCliente = _empresaCliente.GetMantenimientoById(IdMantenimiento);

            if (empresaCliente == null)
            {
                return NotFound($"no se econtrol el id:{IdMantenimiento} no encontrado.");
            }

            return Ok(empresaCliente);
        }

        [HttpPost("InsertMantenimiento")]
        public IActionResult Create([FromBody] Mantenimiento NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.InsertMantenimiento(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateMantenimiento")]
        public IActionResult Update([FromBody] Mantenimiento UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.UpdateMantenimiento(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteMantenimiento")]
        public IActionResult Delete([FromBody] Mantenimiento DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.DeleteMantenimiento(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteMantenimientoById/{IdMantenimiento}")]
        public IActionResult DeleteById(int IdMantenimiento)
        {
            try
            {
                _empresaCliente.DeleteMantenimientoById(IdMantenimiento);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }
    }
}
