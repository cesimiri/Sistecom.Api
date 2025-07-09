using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
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

        [HttpPost("Create")]
        public IActionResult Create([FromBody] stockBodegaDTO item)
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


        [HttpGet("GetStockBodegaPaginados")]
        public IActionResult GetStockBodegaPaginados(
        int pagina = 1,
        int pageSize = PaginadorHelper.NumeroDeDatosPorPagina,
        string? filtro = null,
        string? estado = null)
        {
            try
            {
                // Llamamos al método que devuelve el paginado (en el servicio)
                var resultado = _service.GetStockBodegaPaginados(pagina, pageSize, filtro, estado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // paginado por bodega
        [HttpGet("GetPaginadosPorBodega")]
        public IActionResult GetPaginadosPorBodega(int idBodega, int pagina = 1, int pageSize = 8, string? filtro = null)
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
        [HttpPost("ProcesarMovimientoStock")]
        public IActionResult ProcesarMovimientoStock([FromBody] List<MovimientosInventarioDTO> movimientos)
        {
            if (!_service.ProcesarMovimientoStock(movimientos, out string error))
                return BadRequest(new { mensaje = "No se pudo procesar el stock", detalle = error });

            return Ok(new { mensaje = "Stock actualizado correctamente" });
        }

    }
}
