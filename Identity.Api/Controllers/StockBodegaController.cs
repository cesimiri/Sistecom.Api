using Identity.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Sistecom.Modelo.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Identity.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockBodegaController : ControllerBase
    {
        private readonly IStockBodega _service;

        public StockBodegaController(IStockBodega service)
        {
            _service = service;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAllStockBodega());
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var item = _service.GetStockBodegaById(id);
            if (item == null)
                return NotFound($"StockBodega con ID {id} no encontrado.");
            return Ok(item);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] StockBodega item)
        {
            if (item == null || !ModelState.IsValid)
                return BadRequest("Datos inválidos.");

            _service.InsertStockBodega(item);
            return Ok(item);
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] StockBodega item)
        {
            if (item == null || !ModelState.IsValid)
                return BadRequest("Datos inválidos.");

            _service.UpdateStockBodega(item);
            return NoContent();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete([FromBody] StockBodega item)
        {
            if (item == null)
                return BadRequest("Datos inválidos.");

            _service.DeleteStockBodega(item);
            return NoContent();
        }

        [HttpDelete("DeleteById/{id}")]
        public IActionResult DeleteById(int id)
        {
            _service.DeleteStockBodegaById(id);
            return NoContent();
        }
    }
}
