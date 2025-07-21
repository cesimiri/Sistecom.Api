using Identity.Api.Interfaces;
using Identity.Api.Reporteria;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Sistecom.Modelo.Database;
using QuestPDF.Infrastructure;


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

        //exportar
        [HttpGet("exportarPDF")]
        public IActionResult ExportarPdf(int idBodega, string? filtro = null, string? correo = null)
        {


            QuestPDF.Settings.License = LicenseType.Community;

            var datos = _service.ObtenerParaExportar(idBodega, filtro);
            if (datos == null || !datos.Any())
                return NotFound("No hay datos para exportar.");

            var pdfBytes = StockBodegaPdfGenerator.Generate(datos, correo);


            if (pdfBytes == null || pdfBytes.Length == 0)
                return BadRequest("El PDF generado está vacío.");

            Response.Headers["Content-Disposition"] = "attachment; filename=\"StockBodega.pdf\"";
            Response.Headers["Access-Control-Expose-Headers"] = "Content-Disposition";
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            return File(pdfBytes, "application/pdf");
        }









    }
}
