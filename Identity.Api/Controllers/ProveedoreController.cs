using Identity.Api.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
