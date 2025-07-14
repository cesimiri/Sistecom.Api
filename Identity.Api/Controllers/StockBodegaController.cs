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



        [HttpPut("Update")]
        public IActionResult Update([FromBody] StockBodega item)
        {
            if (item == null || !ModelState.IsValid)
                return BadRequest("Datos inválidos.");

            _service.UpdateStockBodega(item);
            return NoContent();
        }

        //[HttpDelete("Delete")]
        //public IActionResult Delete([FromBody] StockBodega item)
        //{
        //    if (item == null)
        //        return BadRequest("Datos inválidos.");

        //    _service.DeleteStockBodega(item);
        //    return NoContent();
        //}

        [HttpDelete("DeleteById/{id}")]
        public IActionResult DeleteById(int id)
        {
            _service.DeleteStockBodegaById(id);
            return NoContent();
        }


        // paginado por bodega
        [HttpGet("GetPaginadosPorBodega/{idBodega}")]
        public IActionResult GetPaginadosPorBodega(int idBodega, [FromQuery] int pagina = 1, [FromQuery] int pageSize = 8, [FromQuery] string? filtro = null)
        {
            try
            {
                var resultado = _service.GetPaginadosPorBodega(idBodega, pagina, pageSize, filtro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Error al obtener el stock por bodega", detalle = ex.Message });
            }
        }


        //Actualizar stock
        //[HttpPost("ProcesarMovimientoStock")]
        //public IActionResult ProcesarMovimientoStock([FromBody] List<MovimientosInventarioDTO> movimientos)
        //{
        //    if (!_service.ProcesarMovimientoStock(movimientos, out string error))
        //        return BadRequest(new { mensaje = "No se pudo procesar el stock", detalle = error });

        //    return Ok(new { mensaje = "Stock actualizado correctamente" });
        //}

    }
}
