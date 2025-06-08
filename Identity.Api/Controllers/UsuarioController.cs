using Identity.Api.Interfaces;
using Identity.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Sistecom.Modelo.Database;

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
            var usuario = _usuario.GetById(id);
            if (usuario == null)
            {
                return NotFound($"Usuario con ese ID: {id} no encontrado.");
            }
            return Ok(usuario);
        }

        [HttpPost("InsertUsuario")]
        public IActionResult Create([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }
                _usuario.InsertUsuario(usuario);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("UpdateUsuario")]
        public IActionResult Update([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }
                _usuario.UpdateUsuario(usuario);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteUsuario")]
        public IActionResult Delete([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }
                _usuario.DeleteUsuario(usuario);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteUsuarioById/{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _usuario.DeleteUsuarioById(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
