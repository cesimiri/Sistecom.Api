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
    public class SuscripcioneController : ControllerBase
    {
        private readonly ISuscripcione _suscripcioneService;

        public SuscripcioneController(ISuscripcione suscripcioneService)
        {
            _suscripcioneService = suscripcioneService;
        }

        [HttpGet("SuscripcionesAll")]
        public IActionResult GetAll()
        {
            var suscripciones = _suscripcioneService.GetAllSuscripciones();
            return Ok(suscripciones);
        }

        [HttpGet("GetSuscripcionById/{idSuscripcion}")]
        public IActionResult GetById(int idSuscripcion)
        {
            var suscripcion = _suscripcioneService.GetSuscripcionById(idSuscripcion);
            if (suscripcion == null)
            {
                return NotFound($"Suscripción con ID {idSuscripcion} no encontrada.");
            }
            return Ok(suscripcion);
        }

        [HttpPost("InsertSuscripcion")]
        public IActionResult InsertSuscripcion([FromBody] SuscripcionDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                _suscripcioneService.InsertSuscripcion(dto);
                return Ok(new { message = "Suscripción guardada exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("UpdateSuscripcion")]
        public IActionResult Update([FromBody] SuscripcionDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                _suscripcioneService.UpdateSuscripcion(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("DeleteSuscripcion")]
        public IActionResult Delete([FromBody] SuscripcionDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                _suscripcioneService.DeleteSuscripcion(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("DeleteSuscripcionById/{idSuscripcion}")]
        public IActionResult DeleteById(int idSuscripcion)
        {
            try
            {
                _suscripcioneService.DeleteSuscripcionById(idSuscripcion);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet("GetSuscripcionPaginados")]
        public IActionResult GetSuscripcionPaginados(
        int pagina = 1,
        int pageSize = PaginadorHelper.NumeroDeDatosPorPagina,
        string? filtro = null,
        string? estado = null)
        {
            try
            {
                // Llamamos al método que devuelve el paginado (en el servicio)
                var resultado = _suscripcioneService.GetSuscripcionPaginados(pagina, pageSize, filtro, estado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        //[HttpGet("GetUsuarioCargo1")]
        //public ActionResult<IEnumerable<UsuarioDTO>> GetUsuarioCargo1()
        //{
        //    var usuarios = _suscripcioneService.GetUsuarioCargo1();
        //    return Ok(usuarios);
        //}


        //exportar
        [HttpGet("exportarPDF")]
        public IActionResult ObtenerSuscripcioneFiltradas(string? filtro = null, string? estado = null, string? correo = null)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var datos = _suscripcioneService.ObtenerSuscripcioneFiltradas(filtro, estado);

            if (datos == null || !datos.Any())
                return NotFound("No hay datos para exportar.");

            var pdfBytes = SuscripcionePdfGenerator.GenerarPdf(datos, correo);

            return File(pdfBytes, "application/pdf", "suscripcioneListado.pdf");
        }

        // Exporar Excel
        [HttpGet("exportarExcel")]
        public IActionResult ExportarEmpresasExcel(string? filtro = null, string? estado = null)
        {
            var datos = _suscripcioneService.ObtenerSuscripcioneFiltradas(filtro, estado);

            if (datos == null || !datos.Any())
                return NotFound("No hay datos para exportar.");

            var excelBytes = SuscripcioneExcelGenerator.GenerarExcel(datos);

            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmpresasListado.xlsx");
        }
    }
}
