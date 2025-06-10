using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Identity.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UsuarioController : Controller
    {
        private readonly IUsuario _usuario;

        public UsuarioController(IUsuario iUsuario)
        {
            _usuario = iUsuario;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllUsuarios")]
        public IActionResult GetAll()
        {

            return Ok(_usuario.GetAllUsuarios);
        }

        [HttpGet("GetUsuarioById/{id}")]
        public IActionResult GetById(int id)
        {

            var usuario = _usuario.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound($"Suscripción con ID {id} no encontrada.");
            }
            return Ok(usuario);
        }

        [HttpPost("InsertUsuario")]
        public IActionResult InsertUsuario([FromBody] UsuarioDTO dto)
        {

            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Datos invalidos");
            }

            try
            {
                _usuario.InsertUsuario(dto);
                return Ok(new { message = "Usuario guardada exitosamente." });
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return BadRequest(new { error = innerMessage });
            }
        }

        [HttpPut("UpdateUsuario")]
        public IActionResult UpdateUsuario([FromBody] UsuarioDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                _usuario.UpdateUsuario(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("DeleteUsuario")]
        public IActionResult Delete([FromBody] UsuarioDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                _usuario.DeleteUsuario(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("DeleteUsuarioById/{idUsuario}")]
        public IActionResult DeleteById(int idUsuario)
        {
            try
            {
                _usuario.DeleteUsuarioById(idUsuario);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
