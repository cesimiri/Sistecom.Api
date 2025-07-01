using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
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


        //Obtener las sucursales por empresa
        [HttpGet("ObtenerSucursalesByRuc/{RucEmpresa}")]
        public IActionResult ObtenerSucursalesByRuc(string RucEmpresa)
        {
            var modelos = _usuario.ObtenerSucursalesByRuc(RucEmpresa);

            if (modelos == null || !modelos.Any())
            {
                return NotFound($"No se encontraron Sucursales con esa Ruc de empresa {RucEmpresa}.");
            }

            return Ok(modelos);
        }

        //Obtener las departamentos por sucursal
        [HttpGet("ObtenerDepartamentosBySucursal/{idSucursal}")]
        public IActionResult ObtenerDepartamentosBySucursal(int idSucursal)
        {
            var modelos = _usuario.ObtenerDepartamentosBySucursal(idSucursal);

            if (modelos == null || !modelos.Any())
            {
                return NotFound($"No se encontraron los departamentos por esa id de sucursal: {idSucursal}.");
            }

            return Ok(modelos);
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

        //[HttpDelete("DeleteUsuario")]
        //public IActionResult Delete([FromBody] UsuarioDTO dto)
        //{
        //    if (dto == null || !ModelState.IsValid)
        //    {
        //        return BadRequest("Datos inválidos.");
        //    }

        //    try
        //    {
        //        _usuario.DeleteUsuario(dto);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { error = ex.Message });
        //    }
        //}

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


        [HttpGet("GetUsuariosPaginados")]
        public IActionResult GetUsuariosPaginados(
        int pagina = 1,
        int pageSize = PaginadorHelper.NumeroDeDatosPorPagina,
        string? filtro = null,
        string? estado = null)
        {
            try
            {
                // Llamamos al método que devuelve el paginado (en el servicio)
                var resultado = _usuario.GetUsuariosPaginados(pagina, pageSize, filtro, estado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
