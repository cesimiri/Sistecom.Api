using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Identity.Api.Reporteria;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Sistecom.Modelo.Database;
using QuestPDF.Infrastructure;

namespace Identity.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmpresaClienteController : Controller
    {
        private readonly IEmpresaCliente _empresaCliente;

        public EmpresaClienteController(IEmpresaCliente iEmpresaCliente)
        {
            _empresaCliente = iEmpresaCliente;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("EmpresasClientesInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_empresaCliente.EmpresasClientesInfoAll);
        }


        [HttpGet("GetEmpresaClienteById/{ruc}")]
        public IActionResult GetEmpresaClienteById(string ruc)
        {
            //var proveedor = _proveedorService.GetProveedorById(idProveedor);

            var empresaCliente = _empresaCliente.GetEmpresaClienteById(ruc);

            if (empresaCliente == null)
            {
                return NotFound($"Empresa Cliente con ID {ruc} no encontrado.");
            }

            return Ok(empresaCliente);

        }

        [HttpPost("InsertEmpresaCliente")]
        //public IActionResult Create([FromBody] EmpresasCliente NewItem)
        //{
        //    try
        //    {
        //        if (NewItem == null || !ModelState.IsValid)
        //        {
        //            return BadRequest("Error: Envio de datos");
        //        }

        //        _empresaCliente.InsertEmpresaCliente(NewItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error:" + ex.Message);
        //    }

        //    //return Ok(NewItem);
        //    return CreatedAtAction(nameof(GetEmpresaClienteById), new { ruc = NewItem.Ruc }, NewItem);

        //}


        public IActionResult Create([FromBody] EmpresasCliente NewItem)
        {
            try
            {
                // Validaciones más específicas
                if (NewItem == null)
                {
                    return BadRequest("Los datos de la empresa cliente son requeridos");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Validación específica del RUC
                if (string.IsNullOrWhiteSpace(NewItem.Ruc))
                {
                    return BadRequest("El RUC es requerido");
                }

                // Verificar si ya existe
                var existente = _empresaCliente.GetEmpresaClienteById(NewItem.Ruc);
                if (existente != null)
                {
                    return Conflict($"Ya existe una empresa cliente con RUC: {NewItem.Ruc}");
                }

                _empresaCliente.InsertEmpresaCliente(NewItem);

                return CreatedAtAction(nameof(GetEmpresaClienteById), new { ruc = NewItem.Ruc }, NewItem);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Error de validación: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Error de operación: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("UpdateEmpresaCliente")]
        public IActionResult Update([FromBody] EmpresasCliente UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.UpdateEmpresaCliente(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteEmpresaCliente")]
        public IActionResult Delete([FromBody] EmpresasCliente DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.DeleteEmpresaCliente(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteEmpresaCliente/{ruc}")]
        public IActionResult DeleteById(string ruc)
        {
            try
            {
                _empresaCliente.DeleteEmpresaClienteById(ruc);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }


        [HttpGet("GetEmpresasPaginados")]
        public IActionResult GetEmpresasPaginados(
        int pagina = 1,
        int pageSize = PaginadorHelper.NumeroDeDatosPorPagina,
        string? filtro = null,
        string? estado = null)
        {
            try
            {
                // Llamamos al método que devuelve el paginado (en el servicio)
                var resultado = _empresaCliente.GetEmpresasPaginados(pagina, pageSize, filtro, estado);

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

            var datos = _empresaCliente.ObtenerEmpresasFiltradas(filtro, estado);

            if (datos == null || !datos.Any())
                return NotFound("No hay datos para exportar.");

            var pdfBytes = EmpresaPdfGenerator.GenerarPdf(datos, correo);

            return File(pdfBytes, "application/pdf", "EmpresasListado.pdf");
        }

    }
}
