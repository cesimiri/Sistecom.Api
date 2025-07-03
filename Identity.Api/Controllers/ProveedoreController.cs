using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ProveedorController : Controller
    {
        private readonly IProveedor _proveedorService;

        public ProveedorController(IProveedor proveedorService)
        {
            _proveedorService = proveedorService;
        }

        [HttpGet("GetAllProveedores")]
        public IActionResult GetAll()
        {
            var proveedores = _proveedorService.GetAllProveedores();
            return Ok(proveedores);
        }

        [HttpGet("GetProveedorById/{idProveedor}")]
        public IActionResult GetProveedorById(int idProveedor)
        {
            var proveedor = _proveedorService.GetProveedorById(idProveedor);

            if (proveedor == null)
            {
                return NotFound($"Proveedor con ID {idProveedor} no encontrado.");
            }

            return Ok(proveedor);
        }

        [HttpPost("InsertProveedor")]
        public IActionResult Insert([FromBody] Proveedore newProveedor)
        {
            if (newProveedor == null || !ModelState.IsValid)
                return BadRequest("Error: Invalid data");

            _proveedorService.InsertProveedor(newProveedor);
            return Ok(newProveedor);
        }

        [HttpPut("UpdateProveedor")]
        public IActionResult Update([FromBody] Proveedore updatedProveedor)
        {
            if (updatedProveedor == null || !ModelState.IsValid)
                return BadRequest("Error: Invalid data");

            _proveedorService.UpdateProveedor(updatedProveedor);
            return NoContent();
        }

        [HttpDelete("DeleteProveedor")]
        public IActionResult Delete([FromBody] Proveedore proveedorToDelete)
        {
            if (proveedorToDelete == null || !ModelState.IsValid)
                return BadRequest("Error: Invalid data");

            _proveedorService.DeleteProveedor(proveedorToDelete);
            return NoContent();
        }

        [HttpDelete("DeleteProveedor/{idProveedor}")]
        public IActionResult DeleteById(int idProveedor)
        {
            _proveedorService.DeleteProveedorById(idProveedor);
            return NoContent();
        }

        [HttpGet("GetProveedorePaginados")]
        public IActionResult GetUsuariosPaginados(
        int pagina = 1,
        int pageSize = PaginadorHelper.NumeroDeDatosPorPagina,
        string? filtro = null,
        string? estado = null)
        {
            try
            {
                // Llamamos al método que devuelve el paginado (en el servicio)
                var resultado = _proveedorService.GetProveedorePaginados(pagina, pageSize, filtro, estado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
