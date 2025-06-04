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

    public class CategoriasProductoController : Controller
    {
        private readonly ICategoriasProducto _empresaCliente;

        public CategoriasProductoController(ICategoriasProducto iCategoriasProducto)
        {
            _empresaCliente = iCategoriasProducto;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("CategoriasProductoInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_empresaCliente.CategoriasProductoInfoAll);
        }


        [HttpGet("GetCategoriasProductoById/{IdCategoriasProducto}")]
        public IActionResult GetCategoriasProductoById(int IdCategoriasProducto)
        {

            var empresaCliente = _empresaCliente.GetCategoriasProductoById(IdCategoriasProducto);

            if (empresaCliente == null)
            {
                return NotFound($"Empresa Cliente con ID {IdCategoriasProducto} no encontrado.");
            }

            return Ok(empresaCliente);
        }

        [HttpPost("InsertCategoriasProducto")]
        public IActionResult Create([FromBody] CategoriasProducto NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.InsertCategoriasProducto(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateCategoriasProducto")]
        public IActionResult Update([FromBody] CategoriasProducto UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.UpdateCategoriasProducto(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteCategoriasProducto")]
        public IActionResult Delete([FromBody] CategoriasProducto DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.DeleteCategoriasProducto(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteCategoriasProductoById/{IdCategoriasProducto}")]
        public IActionResult DeleteById(int IdCategoriasProducto)
        {
            try
            {
                _empresaCliente.DeleteCategoriasProductoById(IdCategoriasProducto);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }
    }
}
