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

    public class OrdenesEntregaController : Controller
    {
        private readonly IOrdenesEntrega _empresaCliente;

        public OrdenesEntregaController(IOrdenesEntrega iOrdenesEntrega)
        {
            _empresaCliente = iOrdenesEntrega;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        //traer todas las solicitudes de compra APROBADAS que no esten registradas aqui en ordenes de Entrega
        [HttpGet("GetSolicitudesAprobadasSinOrden")]
        public IActionResult GetById()
        {
            var detalles = _empresaCliente.GetSolicitudesAprobadasSinOrden();

            if (detalles == null || !detalles.Any())
            {
                return NotFound($"No se encontraron Solicitudes");
            }

            return Ok(detalles);
        }


        //obtener un usuario por su id que traiga depoaprtamentos y nombre de departamento
        [HttpGet("GetUsuarioDetalleById/{cedula}")]
        public IActionResult GetById(string cedula)
        {
            var detalles = _empresaCliente.GetUsuarioDetalleById(cedula);

            if (detalles == null || !detalles.Any())
            {
                return NotFound($"No se encontraron asignaciones para la cédula {cedula}");
            }

            return Ok(detalles);
        }


        [HttpGet("GetOrdenesEntregaById/{IdOrdenesEntrega}")]
        public IActionResult GetOrdenesEntregaById(int IdOrdenesEntrega)
        {

            var empresaCliente = _empresaCliente.GetOrdenesEntregaById(IdOrdenesEntrega);

            if (empresaCliente == null)
            {
                return NotFound($"No se encontró la Orden de Entrega con el ID: {IdOrdenesEntrega}.");
            }

            return Ok(empresaCliente);
        }

        [HttpPost("InsertOrdenesEntrega")]
        public IActionResult Create([FromBody] OrdenesEntregaDTO newItem)
        {
            try
            {
                if (newItem == null)
                    return BadRequest("Error: No se recibieron datos en la solicitud.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empresaCliente.InsertOrdenesEntrega(newItem);

                // 👇 Aquí devolvemos SOLO el IdOrden
                return Ok(new
                {
                    message = "Orden creada correctamente",
                    idOrden = newItem.IdOrden
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error interno del servidor",
                    detalle = ex.InnerException?.Message ?? ex.Message
                });
            }
        }



        [HttpPut("UpdateOrdenesEntrega")]
        public IActionResult Update([FromBody] OrdenesEntregaDTO UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.UpdateOrdenesEntrega(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteOrdenesEntregaById/{IdOrdenesEntega}")]
        public IActionResult DeleteById(int IdOrdenesEntega)
        {
            try
            {
                _empresaCliente.DeleteOrdenesEntregaById(IdOrdenesEntega);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        //generar PDF Ordenes automaticamente
        [HttpGet("ObtenerOrdenesConDetallesAsync/{idOrdenes}")]
        public async Task<IActionResult> DescargarOrdenesPdf(int idOrdenes)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            // 1. Obtener orden y detalles
            var (ordenes, detalles) = await _empresaCliente.ObtenerOrdenesConDetallesAsync(idOrdenes);

            // 2. Validar que exista
            if (ordenes == null)
                return NotFound("Orden no encontrada.");

            // 3. Generar PDF
            var pdfBytes = OrdenesEntregaPdfGenerator.GenerarPdf(ordenes, detalles);

            if (pdfBytes == null || pdfBytes.Length == 0)
                return StatusCode(500, "Error al generar el PDF.");

            // 4. Crear nombre del archivo con el número de orden
            var numeroOrdenNormalizado = !string.IsNullOrWhiteSpace(ordenes.NumeroOrden)
                ? ordenes.NumeroOrden.Replace(" ", "_")
                : ordenes.IdOrden.ToString();

            var fileName = $"Orden_{numeroOrdenNormalizado}_{DateTime.Now:yyyyMMdd}.pdf";

            // 5. Retornar el PDF
            return File(pdfBytes, "application/pdf", fileName);
        }



        //PAGINADO
        [HttpGet("GetOrdenesEntregaPaginadas")]
        public IActionResult GetOrdenesEntregaPaginadas(
            int pagina = 1,
            int pageSize = 8,
            string? codigoActivo = null,
            string? estadoActivo = null)
        {
            var resultado = _empresaCliente.GetOrdenesEntregaPaginadas(
                pagina, pageSize,
                codigoActivo, estadoActivo);

            return Ok(resultado);
        }
    }
}
