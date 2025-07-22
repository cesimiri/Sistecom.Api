using Identity.Api.DTO;
using Identity.Api.Interfaces;
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

    public class MovimientosInventarioController : Controller
    {
        private readonly IMovimientosInventario _bodega;

        public MovimientosInventarioController(IMovimientosInventario iMovimientosInventario)
        {
            _bodega = iMovimientosInventario;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]




        ///----------------//////////////////////-------------------------////////////////--------------//
        [HttpPost("RegistrarMovimientos")]
        public IActionResult Registrar([FromBody] List<MovimientosInventarioDTO> movimientos)
        {
            try
            {
                var exito = _bodega.RegistrarMovimientos(movimientos, out string error);

                if (!exito)
                    return BadRequest(new { error });

                return Ok(new { mensaje = "Movimientos registrados correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }



        [HttpGet("GetPaginados")]
        public IActionResult GetPaginados(
        int pagina = 1,
        int pageSize = 10,
        string? tipoMovimiento = null,
        int? idBodega = null,
        string? nombreProducto = null,
        DateTime? desde = null,
        DateTime? hasta = null,
        string? ordenColumna = null,
        bool ordenAscendente = true,
        int? idProducto = null) // nuevo
        {
            var resultado = _bodega.GetPaginados(pagina, pageSize, tipoMovimiento, idBodega, nombreProducto, desde, hasta, ordenColumna, ordenAscendente, idProducto);
            return Ok(resultado);
        }




        // Endpoint para traer las solicitudes para dar de baja en salidas, NO usadas en movimientos
        [HttpGet("SolicitudesDeSalida")]
        public async Task<ActionResult<List<SolicitudesCompraDTO>>> ObtenerSolicitudesNoUsadasAsync()
        {
            var solicitudes = await _bodega.ObtenerSolicitudesNoUsadasAsync();
            return Ok(solicitudes);
        }

        [HttpGet("ObtenerDetalleSolicitudAsync/{idSolicitud}")]
        public async Task<ActionResult<List<DetalleSolicitudDTO>>> ObtenerDetalleSolicitudAsync(int idSolicitud)
        {
            var detalles = await _bodega.ObtenerDetalleSolicitudAsync(idSolicitud);
            return Ok(detalles);
        }


        // Endpoint para traer facturas NO usadas en movimientos
        [HttpGet("disponibles")]
        public async Task<ActionResult<List<FacturasCompraDTO>>> GetFacturasNoUsadas()
        {
            var facturas = await _bodega.ObtenerFacturasNoUsadasAsync();
            return Ok(facturas);
        }

        // Endpoint para traer detalles de factura seleccionada
        [HttpGet("detalle/{idFactura}")]
        public async Task<ActionResult<List<DetalleFacturaCompraDTO>>> GetDetalleFactura(int idFactura)
        {
            var detalles = await _bodega.ObtenerDetalleFacturaAsync(idFactura);
            return Ok(detalles);
        }


        //exportar 
        [HttpGet("exportarPDF")]
        public IActionResult ExportarEmpresasPdf(
            string? tipoMovimiento = null,
            int? idBodega = null,
            string? nombreProducto = null,
            DateTime? desde = null,
            DateTime? hasta = null,
            string? correo = null)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var datos = _bodega.ExportarHistorialMovimientoPDFAsync(tipoMovimiento, idBodega, nombreProducto, desde, hasta);

            if (datos == null || !datos.Any())
                return NotFound("No hay datos para exportar.");

            var pdfBytes = MovimientoInventarioPdfGenerator.GenerarPdf(datos, correo, desde, hasta, tipoMovimiento, idBodega, nombreProducto);

            return File(pdfBytes, "application/pdf", "MovimientosInventarioReporte.pdf");
        }

    }
}
