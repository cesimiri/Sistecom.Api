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
    public class ContratoController : Controller
    {
        private readonly IContrato _contratoService;

        public ContratoController(IContrato contratoService)
        {
            _contratoService = contratoService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllContratos")]
        public IActionResult GetAll()
        {
            return Ok(_contratoService.ContratosInfoAll);
        }

        [HttpGet("GetContratoById/{idContrato}")]
        public IActionResult GetContratoById(int idContrato)
        {
            var contrato = _contratoService.GetContratoById(idContrato);

            if (contrato == null)
                return NotFound($"Contrato con ID {idContrato} no encontrado.");

            return Ok(contrato);
        }

        [HttpPost("InsertContrato")]
        public IActionResult Insert([FromBody] Contrato newContrato)
        {
            if (newContrato == null || !ModelState.IsValid)
                return BadRequest("Error: Datos inválidos");

            _contratoService.InsertContrato(newContrato);
            return Ok(newContrato);
        }

        [HttpPut("UpdateContrato")]
        public IActionResult Update([FromBody] Contrato updatedContrato)
        {
            if (updatedContrato == null || !ModelState.IsValid)
                return BadRequest("Error: Datos inválidos");

            _contratoService.UpdateContrato(updatedContrato);
            return NoContent();
        }

        [HttpDelete("DeleteContrato")]
        public IActionResult Delete([FromBody] Contrato contratoToDelete)
        {
            if (contratoToDelete == null || !ModelState.IsValid)
                return BadRequest("Error: Datos inválidos");

            _contratoService.DeleteContrato(contratoToDelete);
            return NoContent();
        }

        [HttpDelete("DeleteContratoById/{idContrato}")]
        public IActionResult DeleteById(int idContrato)
        {
            _contratoService.DeleteContratoById(idContrato);
            return NoContent();
        }
    }
}
