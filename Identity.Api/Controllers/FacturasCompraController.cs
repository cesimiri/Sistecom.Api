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

        // 📌 Subir imagen de factura
        [HttpPost("UploadFacturaImage")]
        public async Task<IActionResult> UploadFacturaImage(
            [FromForm] IFormFile file,
            [FromForm] string numeroFactura,
            [FromForm] string rucProveedor)
        {
            try
            {
                // 1. Validaciones de datos
                if (file == null || file.Length == 0)
                    return BadRequest("No se recibió ningún archivo o está vacío.");

                if (string.IsNullOrWhiteSpace(numeroFactura))
                    return BadRequest("El número de factura es obligatorio.");

                if (string.IsNullOrWhiteSpace(rucProveedor))
                    return BadRequest("El RUC del proveedor es obligatorio.");

                // 2. Validación de extensiones permitidas
                var extension = Path.GetExtension(file.FileName)?.ToLower();
                var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
                if (!extensionesPermitidas.Contains(extension))
                    return BadRequest($"Formato de archivo no permitido. Solo: {string.Join(", ", extensionesPermitidas)}");

                // 3. Construcción del nombre base
                var nombreBase = $"Factura-{rucProveedor}-{numeroFactura}";

                // 4. Ruta física de destino
                var uploadsFolder = @"C:\inetpub\wwwroot\facturas";

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                // 5. Contar imágenes existentes y asignar nombre único
                var existentes = Directory.GetFiles(uploadsFolder, $"{nombreBase}-*.*").Length;
                var nombreFinal = $"{nombreBase}-{existentes + 1}{extension}";
                var rutaCompleta = Path.Combine(uploadsFolder, nombreFinal);

                // 6. Guardar archivo en disco
                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // 7. URL pública
                var urlImagen = $"http://192.168.120.241/facturas/{nombreFinal}";

                // 8. Respuesta
                return Ok(new
                {
                    mensaje = "Imagen guardada exitosamente.",
                    nombreArchivo = nombreFinal,
                    url = urlImagen
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(500, $"Error de permisos al guardar la imagen: {ex.Message}");
            }
            catch (IOException ex)
            {
                return StatusCode(500, $"Error de entrada/salida al guardar la imagen: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // 📌 Buscar imágenes por número de factura
        [HttpGet("BuscarImagenesFactura")]
        public IActionResult BuscarImagenesFactura([FromQuery] string numeroFactura, [FromQuery] string rucProveedor)
        {
            try
            {
                // Carpeta física
                var uploadsFolder = @"C:\inetpub\wwwroot\facturas";
                var nombreBase = $"Factura-{rucProveedor}-{numeroFactura}";

                if (!Directory.Exists(uploadsFolder))
                    return Ok(new List<string>());

                var archivos = Directory
                    .GetFiles(uploadsFolder, $"{nombreBase}-*.*")
                    .Select(path => Path.GetFileName(path))
                    .Select(nombre => $"http://192.168.120.241/facturas/{nombre}") // URL pública
                    .ToList();

                return Ok(archivos);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(500, $"Error de permisos al leer las imágenes: {ex.Message}");
            }
            catch (IOException ex)
            {
                return StatusCode(500, $"Error de entrada/salida al leer las imágenes: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // 📌 Eliminar imagen por nombre
        [HttpDelete("EliminarFacturaImagen")]
        public IActionResult EliminarFacturaImagen([FromQuery] string nombreArchivo)
        {
            try
            {
                var uploadsFolder = @"C:\inetpub\wwwroot\facturas";
                var rutaCompleta = Path.Combine(uploadsFolder, nombreArchivo);

                if (!System.IO.File.Exists(rutaCompleta))
                    return NotFound("La imagen no existe.");

                System.IO.File.Delete(rutaCompleta);

                return Ok(new { mensaje = "Imagen eliminada correctamente." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(500, $"Error de permisos al eliminar la imagen: {ex.Message}");
            }
            catch (IOException ex)
            {
                return StatusCode(500, $"Error de entrada/salida al eliminar la imagen: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        // 📌 Debug: verificar si existe la imagen
        [HttpGet("debug/ruta-base")]
        public IActionResult DebugRutaBase()
        {
            var basePath = Directory.GetCurrentDirectory();
            return Ok(new { basePath });
        }


    }
}
