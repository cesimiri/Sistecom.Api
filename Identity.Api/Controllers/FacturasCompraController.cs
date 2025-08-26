using FluentFTP;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Identity.Api.Reporteria;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Infrastructure;
using System.Net;

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
                // 1️⃣ Validaciones de datos
                if (file == null || file.Length == 0)
                    return BadRequest("No se recibió ningún archivo o está vacío.");

                if (string.IsNullOrWhiteSpace(numeroFactura))
                    return BadRequest("El número de factura es obligatorio.");

                if (string.IsNullOrWhiteSpace(rucProveedor))
                    return BadRequest("El RUC del proveedor es obligatorio.");

                // 2️⃣ Validación de extensiones permitidas
                var extension = Path.GetExtension(file.FileName)?.ToLower();
                var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
                if (!extensionesPermitidas.Contains(extension))
                    return BadRequest($"Formato de archivo no permitido. Solo: {string.Join(", ", extensionesPermitidas)}");

                // 3️⃣ Nombre del archivo final
                string SanitizePath(string input) =>
                    string.Concat(input.Where(c => !Path.GetInvalidFileNameChars().Contains(c)));

                var nombreArchivo = $"Factura-{SanitizePath(rucProveedor)}-{SanitizePath(numeroFactura)}{extension}";

                // 4️⃣ Configuración FTP
                string ftpUrl = "ftp://192.168.120.241/facturas/"; // FTP completo con carpeta
                string ftpUser = "gmoraadmin";
                string ftpPass = "Geo100100.";

                // Crear carpeta si no existe
                FtpWebRequest createDirRequest = (FtpWebRequest)WebRequest.Create(ftpUrl);
                createDirRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                createDirRequest.Credentials = new NetworkCredential(ftpUser, ftpPass);
                try { using var resp = (FtpWebResponse)createDirRequest.GetResponse(); } catch { /* ignora error si ya existe */ }

                // 5️⃣ Subir archivo (reemplaza si existe)
                string uploadUrl = ftpUrl + nombreArchivo;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uploadUrl);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpUser, ftpPass);
                request.UseBinary = true;
                request.UsePassive = true;

                using (var fileStream = file.OpenReadStream())
                using (var ftpStream = request.GetRequestStream())
                {
                    await fileStream.CopyToAsync(ftpStream);
                }

                // 6️⃣ URL pública (HTTP)
                var urlPublica = $"http://192.168.120.241/facturas/{nombreArchivo}";

                return Ok(new
                {
                    mensaje = "Imagen subida exitosamente (reemplazada si ya existía).",
                    nombreArchivo,
                    url = urlPublica
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al subir imagen por FTP: {ex.Message}");
            }
        }


        // 📌 Buscar imágenes por número de factura
        [HttpGet("BuscarImagenesFactura")]
        public IActionResult BuscarImagenesFactura([FromQuery] string numeroFactura, [FromQuery] string rucProveedor)
        {
            string host = "192.168.120.241";
            string user = "gmoraadmin";
            string pass = "Geo100100.";
            string basePath = "/facturas";

            try
            {
                using var client = new FtpClient(host, new NetworkCredential(user, pass));
                client.Connect();

                string nombreBase = $"Factura-{rucProveedor}-{numeroFactura}";

                // Listar archivos en la carpeta FTP
                var archivos = client.GetListing(basePath)
                .Where(f => f.Type == FtpObjectType.File && f.Name.StartsWith(nombreBase))
                .Select(f => $"http://192.168.120.241/facturas/{f.Name}") // URL pública
                .ToList();

                return Ok(archivos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al buscar imágenes por FTP: {ex.Message}");
            }
        }

        // 📌 Buscar imágenes por número de factura
        [HttpDelete("EliminarFacturaImagen")]
        public IActionResult EliminarFacturaImagen([FromQuery] string nombreArchivo)
        {
            string host = "192.168.120.241";
            string user = "gmoraadmin";
            string pass = "Geo100100.";
            string basePath = "/facturas";

            try
            {
                using var client = new FtpClient(host, new NetworkCredential(user, pass));
                client.Connect();

                string rutaArchivo = $"{basePath}/{nombreArchivo}";

                if (!client.FileExists(rutaArchivo))
                    return NotFound("La imagen no existe en el FTP.");

                client.DeleteFile(rutaArchivo);

                return Ok(new { mensaje = "Imagen eliminada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar imagen por FTP: {ex.Message}");
            }
        }

        [HttpGet("GetFacturasCompraByNumeroFactura/{numeroFactura}")]
        public IActionResult GetFacturasCompraByNumeroFactura(string numeroFactura)
        {
            ;

            var facturasCompra = _facturasCompra.GetFacturasCompraByNumeroFactura(numeroFactura);

            if (facturasCompra == null)
            {
                return NotFound($"La factura con ID {numeroFactura} no se encuentra registrada.");
            }

            return Ok(facturasCompra);
        }

        // probar TP Facturas si lelga y si carpeta esta creada
        [HttpGet("ProbarFtpFacturas")]
        public IActionResult ProbarFtpFacturas()
        {
            string host = "192.168.120.241";        // Host del FTP
            string user = "gmoraadmin";             // Usuario FTP
            string pass = "Geo100100.";              // Contraseña FTP
            string basePath = "/facturas";          // Carpeta que quieres verificar

            var log = new List<string>();
            FtpClient client = null;

            try
            {
                // 🔹 Inicializar cliente FTP
                client = new FtpClient(host, new NetworkCredential(user, pass));
                log.Add("Cliente FTP creado.");

                // 🔹 Conectar al servidor FTP
                client.Connect();
                log.Add("Conexión al servidor FTP establecida.");

                if (!client.IsConnected)
                {
                    log.Add("Autenticación fallida.");
                    return StatusCode(403, new { mensaje = "Error de autenticación FTP", log });
                }
                log.Add("Autenticación exitosa.");

                // 🔹 Verificar existencia de la carpeta y crear si no existe
                if (!client.DirectoryExists(basePath))
                {
                    log.Add($"Carpeta '{basePath}' no existe. Creándola...");
                    client.CreateDirectory(basePath);
                    log.Add($"Carpeta '{basePath}' creada correctamente.");
                }
                else
                {
                    log.Add($"Carpeta '{basePath}' encontrada.");
                }

                // 🔹 Listar archivos dentro de la carpeta
                var archivos = client.GetListing(basePath);
                log.Add($"Se encontraron {archivos.Length} archivos en '{basePath}'.");

                // 🔹 Retornar información
                return Ok(new
                {
                    mensaje = "Conexión FTP exitosa ✅",
                    cantidadArchivos = archivos.Length,
                    ejemplos = archivos.Take(5).Select(a => a.FullName),
                    log
                });
            }
            catch (FluentFTP.Exceptions.FtpCommandException ftpEx)
            {
                log.Add($"Error FTP: Código {ftpEx.CompletionCode}, Mensaje: {ftpEx.Message}");
                return StatusCode(403, new { mensaje = "Error de autenticación FTP", log });
            }
            catch (Exception ex)
            {
                log.Add($"Error inesperado: {ex.Message}");
                return StatusCode(500, new { mensaje = "Error inesperado", log });
            }
            finally
            {
                client?.Dispose();
            }
        }
    }
}
