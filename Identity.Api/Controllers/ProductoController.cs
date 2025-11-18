using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Identity.Api.Reporteria;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;

namespace Identity.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ProductoController : Controller
    {
        private readonly IProducto _empresaCliente;

        public ProductoController(IProducto iProducto)
        {
            _empresaCliente = iProducto;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("ProductoInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_empresaCliente.GetAllProducto);
        }


        [HttpGet("GetProductoById/{IdProducto}")]
        public IActionResult GetProductoById(int idProducto)
        {

            var empresaCliente = _empresaCliente.GetProductoById(idProducto);

            if (empresaCliente == null)
            {
                return NotFound($"Producto con ese ID: {idProducto} no encontrado.");
            }

            return Ok(empresaCliente);
        }
        // trae todos los modelos con esa marca
        [HttpGet("GetModelosByIdMarca/{idMarca}")]
        public IActionResult GetModelosByIdMarca(int idMarca)
        {
            var modelos = _empresaCliente.GetModelosByIdMarca(idMarca);

            if (modelos == null || !modelos.Any())
            {
                return NotFound($"No se encontraron modelos para la marca con ID: {idMarca}.");
            }

            return Ok(modelos);
        }

        [HttpPost("InsertProducto")]
        public IActionResult Create([FromBody] ProductoDTO NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.InsertProducto(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateProducto")]
        public IActionResult Update([FromBody] ProductoDTO updItem)
        {
            if (updItem == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            if (!ModelState.IsValid)
                return BadRequest("El modelo de datos no es válido.");

            try
            {
                _empresaCliente.UpdateProducto(updItem);
                return Ok(new { message = "Producto actualizado correctamente." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                // Capturamos el mensaje del inner exception para más detalle
                var detalleError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, $"Error al actualizar el producto en la base de datos: {detalleError}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }



        [HttpDelete("DeleteProductoById/{IdProducto}")]
        public IActionResult DeleteById(int IdProducto)
        {
            try
            {
                _empresaCliente.DeleteProductoById(IdProducto);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpGet("GetProductoPaginados")]
        public IActionResult GetProductoPaginados(
        int pagina = 1,
        int pageSize = PaginadorHelper.NumeroDeDatosPorPagina,
        string? filtro = null,
        string? estado = null)
        {
            try
            {
                // Llamamos al método que devuelve el paginado (en el servicio)
                var resultado = _empresaCliente.GetProductoPaginados(pagina, pageSize, filtro, estado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        //exportar
        [HttpGet("exportarPDF")]
        public IActionResult ExportarEmpresasPdf(string? filtro = null, string? estado = null, string? correo = null)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var datos = _empresaCliente.ObtenerProductoFiltradas(filtro, estado);

            if (datos == null || !datos.Any())
                return NotFound("No hay datos para exportar.");

            var pdfBytes = ProductoPdfGenerator.GenerarPdf(datos, correo);

            return File(pdfBytes, "application/pdf", "EmpresasListado.pdf");
        }
    }
}
