using Identity.Api.Interfaces;
using Identity.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Sistecom.Modelo.Database;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class OrdenesEntegaController : Controller
    {
        private readonly IOrdenesEntrega _empresaCliente;

        public OrdenesEntegaController(IOrdenesEntrega iOrdenesEntrega)
        {
            _empresaCliente = iOrdenesEntrega;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("OrdenesEntregaInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_empresaCliente.OrdenesEntregaInfoAll);
        }


        [HttpGet("GetOrdenesEntregaById/{IdOrdenesEntrega}")]
        public IActionResult GetOrdenesEntregaById(int IdOrdenesEntrega)
        {

            var empresaCliente = _empresaCliente.GetOrdenesEntregaById(IdOrdenesEntrega);

            if (empresaCliente == null)
            {
                return NotFound($"No se encontró la Orden de Entrega con el ID: {IdOrdenesEntrega}.");
            }

            return Ok(empresaCliente);
        }

        [HttpPost("InsertOrdenesEntrega")]
        public IActionResult Create([FromBody] OrdenesEntrega NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.InsertOrdenesEntrega(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateOrdenesEntrega")]
        public IActionResult Update([FromBody] OrdenesEntrega UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.UpdateOrdenesEntrega(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteOrdenesEntrega")]
        public IActionResult Delete([FromBody] OrdenesEntrega DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.DeleteOrdenesEntrega(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteOrdenesEntregaById/{IdOrdenesEntega}")]
        public IActionResult DeleteById(int IdOrdenesEntega)
        {
            try
            {
                _empresaCliente.DeleteOrdenesEntregaById(IdOrdenesEntega);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }
    }
}
