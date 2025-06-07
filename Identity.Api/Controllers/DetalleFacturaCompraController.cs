using Identity.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleFacturaCompraController : Controller
    {
        private readonly IDetalleFacturaCompra _detalleFacturaService;

        public DetalleFacturaCompraController(IDetalleFacturaCompra detalleFacturaService)
        {
            _detalleFacturaService = detalleFacturaService;
        }

        [HttpGet("GetAllDetallesFacturaCompra")]
        public IActionResult GetAll()
        {
            return Ok(_detalleFacturaService.DetallesFacturaCompraInfoAll);
        }

        [HttpGet("GetDetalleFacturaCompraById/{id}")]
        public IActionResult GetById(int id)
        {
            var detalle = _detalleFacturaService.GetDetalleFacturaCompraById(id);
            if (detalle == null)
                return NotFound($"Detalle con ID {id} no encontrado.");
            return Ok(detalle);
        }

        [HttpPost("InsertDetalleFacturaCompra")]
        public IActionResult Insert([FromBody] DetalleFacturaCompra NewItem)
        {
            if (NewItem == null || !ModelState.IsValid)
                return BadRequest("Datos inválidos.");

            _detalleFacturaService.InsertDetalleFacturaCompra(NewItem);
            return Ok(NewItem);
        }

        [HttpPut("UpdateDetalleFacturaCompra")]
        public IActionResult Update([FromBody] DetalleFacturaCompra UpdItem)
        {
            if (UpdItem == null || !ModelState.IsValid)
                return BadRequest("Datos inválidos.");

            _detalleFacturaService.UpdateDetalleFacturaCompra(UpdItem);
            return NoContent();
        }

        [HttpDelete("DeleteDetalleFacturaCompra")]
        public IActionResult Delete([FromBody] DetalleFacturaCompra DelItem)
        {
            if (DelItem == null || !ModelState.IsValid)
                return BadRequest("Datos inválidos.");

            _detalleFacturaService.DeleteDetalleFacturaCompra(DelItem);
            return NoContent();
        }

        [HttpDelete("DeleteDetalleFacturaCompra/{id}")]
        public IActionResult DeleteById(int id)
        {
            _detalleFacturaService.DeleteDetalleFacturaCompraById(id);
            return NoContent();
        }
    }
}
