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
    public class EmpresaClienteController : Controller
    {
        private readonly IEmpresaCliente _empresaCliente;

        public EmpresaClienteController(IEmpresaCliente iEmpresaCliente)
        {
            _empresaCliente = iEmpresaCliente;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("EmpresasClientesInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_empresaCliente.EmpresasClientesInfoAll);
        }

        
        [HttpGet("GetEmpresaClienteById/{idEmpresaCliente}")]
        public IActionResult GetEmpresaClienteById(int idEmpresaCliente)
        {
            //var proveedor = _proveedorService.GetProveedorById(idProveedor);

            var empresaCliente = _empresaCliente.GetEmpresaClienteById(idEmpresaCliente);

            if (empresaCliente == null)
            {
                return NotFound($"Empresa Cliente con ID {idEmpresaCliente} no encontrado.");
            }

            return Ok(empresaCliente);
        }

        [HttpPost("InsertEmpresaCliente")]
        public IActionResult Create([FromBody] EmpresasCliente NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.InsertEmpresaCliente(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateEmpresaCliente")]
        public IActionResult Update([FromBody] EmpresasCliente UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.UpdateEmpresaCliente(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteEmpresaCliente")]
        public IActionResult Delete([FromBody] EmpresasCliente DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.DeleteEmpresaCliente(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteEmpresaCliente/{IdRegistrado}")]
        public IActionResult DeleteById(int IdRegistrado)
        {
            try
            {
                _empresaCliente.DeleteEmpresaClienteById(IdRegistrado);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }
    }
}
