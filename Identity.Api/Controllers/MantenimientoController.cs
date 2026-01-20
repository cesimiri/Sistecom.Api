using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    //[Authorize]
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
        public IActionResult Create([FromBody] MantenimientoDTO NewItem)
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
        public IActionResult Update([FromBody] MantenimientoDTO UpdItem)
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


        [HttpDelete("DeleteMantenimientoById/{IdMantenimiento}")]
        public IActionResult DeleteById(int IdMantenimiento)
        {
            try
            {
                _empresaCliente.DeleteMantenimientoById(IdMantenimiento);

                return Ok(new
                {
                    mensaje = "Mantenimiento eliminado correctamente",
                    id = IdMantenimiento
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }


        //busqueda de tecnico por cedula 

        [HttpGet("ObtenerApellidosNombreByCedula/{cedula}")]
        public IActionResult GetById(string cedula)
        {

            var usuario = _empresaCliente.ObtenerApellidosNombreByCedula(cedula);
            if (usuario == null)
            {
                return NotFound($"Suscripción con ID {cedula} no encontrada.");
            }
            return Ok(usuario);
        }

        //obtener activo por oodigo activo 
        [HttpGet("ObtnerActivoByCodigo/{codigoActivo}")]
        public IActionResult GetByCodigo(string codigoActivo)
        {

            var usuario = _empresaCliente.ObtnerActivoByCodigo(codigoActivo);
            if (usuario == null)
            {
                return NotFound($"Activo con {codigoActivo} no encontrada.");
            }
            return Ok(usuario);
        }

        //paginado
        [HttpGet("GetMantenimientoPaginados")]
        public IActionResult GetMantenimientoPaginados(
        int pagina = 1,
        int pageSize = 8,
        string? filtro = null,
        string? estadoActivo = null)
        {
            var resultado = _empresaCliente.GetMantenimientoPaginados(
                pagina,
                pageSize,
                filtro,
                estadoActivo
            );

            return Ok(resultado);
        }

    }
}
