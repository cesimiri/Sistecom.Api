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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsuarioDetalleController : Controller
    {
        private readonly IUsuarioDetalle _usuarioDetalle;

        public UsuarioDetalleController(IUsuarioDetalle iusuarioDetalle)
        {
            _usuarioDetalle = iusuarioDetalle;
        }

        [HttpGet("GetAllUsuarioDetalle")]
        public IActionResult GetAll()
        {

            return Ok(_usuarioDetalle.GetAllUsuarioDetalle);
        }

        [HttpGet("GetUsuarioDetalleById/{cedula}")]
        public IActionResult GetById(string cedula)
        {

            var usuario = _usuarioDetalle.GetUsuarioDetalleById(cedula);
            if (usuario == null)
            {
                return NotFound($"Suscripción con ID {cedula} no encontrada.");
            }
            return Ok(usuario);
        }

        [HttpPost("InsertUsuarioDetalle")]
        public IActionResult InsertUsuario([FromBody] UsuarioDetalleDTO dto)
        {

            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Datos invalidos");
            }

            try
            {
                _usuarioDetalle.InsertUsuarioDetalle(dto);
                return Ok(new { message = "Usuario guardada exitosamente." });
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return BadRequest(new { error = innerMessage });
            }
        }

        [HttpPut("UpdateUsuarioDetalle")]
        public IActionResult UpdateUsuario([FromBody] UsuarioDetalleDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                _usuarioDetalle.UpdateUsuarioDetalle(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("DeleteUsuarioDetalleById/{cedula}")]
        public IActionResult DeleteById(string cedula)
        {
            try
            {
                _usuarioDetalle.DeleteUsuarioDetalleById(cedula);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("GetUsuarioDetallePaginados")]
        public IActionResult GetUsuariosPaginados(
        int pagina = 1,
        int pageSize = PaginadorHelper.NumeroDeDatosPorPagina,
        string? filtro = null,
        string? estado = null)
        {
            try
            {
                // Llamamos al método que devuelve el paginado (en el servicio)
                var resultado = _usuarioDetalle.GetUsuarioDetallePaginados(pagina, pageSize, filtro, estado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        //exportar PDF
        [HttpGet("exportarPDF")]
        public IActionResult ExportarUsuarioDetallePdf(string? filtro = null, string? estado = null, string? correo = null)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var datos = _usuarioDetalle.ObtenerUsuarioDetalleFiltradas(filtro, estado);

            if (datos == null || !datos.Any())
                return NotFound("No hay datos para exportar.");

            var pdfBytes = UsuarioDetallePdfGenerator.GenerarPdf(datos, correo);

            return File(pdfBytes, "application/pdf", "ListadoUsuario.pdf");
        }


        // Exporar Excel
        [HttpGet("exportarExcel")]
        public IActionResult ExportarUsuarioDetalleExcel(string? filtro = null, string? estado = null)
        {
            var datos = _usuarioDetalle.ObtenerUsuarioDetalleFiltradas(filtro, estado);

            if (datos == null || !datos.Any())
                return NotFound("No hay datos para exportar.");

            var excelBytes = UsuarioDetalleExcelGenerator.GenerarExcel(datos);

            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ListadoUsuario.xlsx");
        }

    }
}
