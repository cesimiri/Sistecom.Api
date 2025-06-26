using Identity.Api.DTO;
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

        [HttpPost("InsertarDetallesMasivos")]
        public IActionResult InsertarDetallesMasivos([FromBody] List<DetalleFacturaCompraDTO> lista)
        {
            try
            {
                if (lista == null || !lista.Any())
                    return BadRequest("La lista está vacía o es nula.");

                _detalleFacturaService.InsertarDetallesMasivos(lista);
                return Ok("Inserción masiva completada.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error en la inserción masiva: " + ex.Message);
            }
        }

        [HttpPut("UpdateDetalleFacturaCompra")]
        public IActionResult Update([FromBody] DetalleFacturaCompra UpdItem)
        {
            if (UpdItem == null || !ModelState.IsValid)
                return BadRequest("Datos inválidos.");

            _detalleFacturaService.UpdateDetalleFacturaCompra(UpdItem);
            return NoContent();
        }

        //[HttpDelete("DeleteDetalleFacturaCompra")]
        //public IActionResult Delete([FromBody] DetalleFacturaCompra DelItem)
        //{
        //    if (DelItem == null || !ModelState.IsValid)
        //        return BadRequest("Datos inválidos.");

        //    _detalleFacturaService.DeleteDetalleFacturaCompra(DelItem);
        //    return NoContent();
        //}

        [HttpDelete("DeleteDetalleFacturaCompraById/{id}")]
        public IActionResult DeleteById(int id)
        {
            _detalleFacturaService.DeleteDetalleFacturaCompraById(id);
            return NoContent();
        }


        [HttpGet("GetDetalleFacturaCompraByIdFactura/{idFactura}")]
        public IActionResult GetDetalleFacturaCompraByIdFactura(int idFactura)
        {
            var detalle = _detalleFacturaService.GetDetalleFacturaCompraByIdFactura(idFactura);
            if (detalle == null || !detalle.Any())
                return NotFound($"No se encontraron detalles con ID de factura {idFactura}.");
            return Ok(detalle);
        }
    }
}
