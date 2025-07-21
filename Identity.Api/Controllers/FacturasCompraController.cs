using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Identity.Api.Reporteria;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Infrastructure;

namespace Identity.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class FacturasCompraController : Controller
    {
        private readonly IFacturasCompra _facturasCompra;

        public FacturasCompraController(IFacturasCompra iFacturasCompra)
        {
            _facturasCompra = iFacturasCompra;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("FacturasCompraInfoAll")]

        public IActionResult GetAll()
        {
            return Ok(_facturasCompra.FacturasCompraInfoAll);
        }

        [HttpGet("GetFacturasCompraById/{idFacturasCompra}")]
        public IActionResult GetFacturasCompraById(int idFacturasCompra)
        {
            ;

            var facturasCompra = _facturasCompra.GetFacturasCompraById(idFacturasCompra);

            if (facturasCompra == null)
            {
                return NotFound($"La factura con ID {idFacturasCompra} no encontrado.");
            }

            return Ok(facturasCompra);
        }

        [HttpPost("InsertFacturasCompra")]
        public IActionResult Create([FromBody] FacturasCompraDTO NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envío de datos inválido.");
                }

                int idGenerado = _facturasCompra.InsertFacturasCompra(NewItem);

                return Ok(idGenerado); // ← retorna solo el ID
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

        [HttpPut("UpdateFacturasCompra")]
        public IActionResult Update([FromBody] FacturasCompraDTO UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _facturasCompra.UpdateFacturasCompra(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }


        //[HttpDelete("DeleteFacturasCompra")]
        //public IActionResult Delete([FromBody] FacturasCompra DelItem)
        //{
        //    try
        //    {
        //        if (DelItem == null || !ModelState.IsValid)
        //        {
        //            return BadRequest("Error: Envio de datos");
        //        }

        //        _facturasCompra.DeleteFacturasCompra(DelItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error:" + ex.Message);
        //    }

        //    return NoContent();
        //}

        [HttpDelete("DeleteFacturasCompraById/{IdFacturasCompra}")]
        public IActionResult DeleteById(int IdFacturasCompra)
        {
            try
            {
                _facturasCompra.DeleteFacturasCompraById(IdFacturasCompra);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }



        [HttpGet("GetFacturasCompraPaginados")]
        public IActionResult GetUsuariosPaginados(
        int pagina = 1,
        int pageSize = PaginadorHelper.NumeroDeDatosPorPagina,
        string? filtro = null,
        string? estado = null)
        {
            try
            {
                // Llamamos al método que devuelve el paginado (en el servicio)
                var resultado = _facturasCompra.GetFacturasCompraPaginados(pagina, pageSize, filtro, estado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        //exportar PDF
        [HttpGet("exportarPDF")]
        public IActionResult ExportarEmpresasPdf(string? filtro = null, string? estado = null, string? correo = null)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var datos = _facturasCompra.ObtenerFacturaCompraFiltradas(filtro, estado);

            if (datos == null || !datos.Any())
                return NotFound("No hay datos para exportar.");

            var pdfBytes = FacturaCompraPdfGenerator.GenerarPdf(datos, correo);

            return File(pdfBytes, "application/pdf", "EmpresasListado.pdf");
        }



        //aqui 
        [HttpGet("ExportarFacturaCompraPdfById/{idFactura}")]
        public async Task<IActionResult> DescargarFacturaPdf(int idFactura)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            // 1. Obtener factura y sus detalles desde el servicio de manera asíncrona
            var (factura, detalles) = await _facturasCompra.ObtenerFacturaConDetallesAsync(idFactura);

            // 2. Validar que la factura exista
            if (factura == null)
                return NotFound("Factura no encontrada.");

            // 3. Obtener correo del usuario autenticado para incluir en el PDF (o correo default)
            string correoUsuario = User.Identity?.Name ?? "correo@ejemplo.com";

            // 4. Generar el PDF (debe devolver un arreglo de bytes)
            var pdfBytes = FacturaPdfGenerator.GenerarPdf(factura, detalles, correoUsuario);

            // 5. Validar que el PDF se generó correctamente
            if (pdfBytes == null || pdfBytes.Length == 0)
                return StatusCode(500, "Error al generar el PDF.");

            // 6. Crear un nombre de archivo para el PDF
            var fileName = $"Factura_{factura.NumeroFactura}_{DateTime.Now:yyyyMMdd}.pdf";

            // 7. Retornar el archivo PDF para descarga
            return File(pdfBytes, "application/pdf", fileName);
        }










    }
}
