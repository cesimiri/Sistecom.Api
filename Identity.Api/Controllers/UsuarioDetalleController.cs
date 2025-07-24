using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
