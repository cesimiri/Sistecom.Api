using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace identity.api.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ConfiguracionesActivoController : ControllerBase
    {
        private readonly IConfiguracionesActivo _service;

        public ConfiguracionesActivoController(IConfiguracionesActivo service)
        {
            _service = service;
        }

        [HttpGet("GetActivosPrincipal")]
        public IActionResult GetActivosPrincipal()
        {
            try
            {
                var usuarios = _service.GetActivosPrincipal();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                // Para desarrollo, devuelve el detalle completo
                return BadRequest(new { error = "Error en GetActivosPrincipal backend", detalle = ex.ToString() });
            }
        }

        [HttpGet("GetActivosComponenetes")]
        public IActionResult GetActivosComponenetes()
        {
            try
            {
                var usuarios = _service.GetActivosComponenetes();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                // Para desarrollo, devuelve el detalle completo
                return BadRequest(new { error = "Error en GetActivosPrincipal backend", detalle = ex.ToString() });
            }
        }



        [HttpPost("InsertarConfiguracion")]
        public IActionResult Insertar([FromBody] ConfiguracionActivoInsertDTO dto)
        {
            if (dto == null || dto.Componentes.Count == 0)
                return BadRequest("Debe enviar al menos un componente.");

            try
            {
                _service.InsertarConfiguracion(dto);
                return Ok(new { message = "Configuración de activo registrada exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        //trae todo lo agrega por ese id
        [HttpGet("GetConfiguracionesByActivo/{idActivoPrincipal}")]
        public IActionResult GetByActivo(int idActivoPrincipal)
        {
            try
            {
                var configs = _service.GetConfiguracionesByActivo(idActivoPrincipal);
                return Ok(configs);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("DeleteConfiguracionesActivoByPrincipal/{idActivoPrincipal}")]
        public IActionResult DeleteConfiguracionesActivoById(int idActivoPrincipal)
        {
            try
            {
                _service.DeleteConfiguracionesActivoById(idActivoPrincipal);
                return NoContent(); // 204 si se borraron
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet("GetConfiguracionesActivoPaginados")]
        public IActionResult GetConfiguracionesActivoPaginados(
            int pagina = 1,
            int pageSize = PaginadorHelper.NumeroDeDatosPorPagina,
            string? filtro = null,
            string? estado = null)
        {
            try
            {
                var resultado = _service.GetConfiguracionesActivoPaginados(pagina, pageSize, filtro, estado);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Error en GetConfiguracionesActivoPaginados", detalle = ex.ToString() });
            }
        }
    }

}

